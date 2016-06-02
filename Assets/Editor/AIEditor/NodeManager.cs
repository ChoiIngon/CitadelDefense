using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AIEditor {
	public class NodeManager : ScriptableObject
	{
		public delegate Node CreateNodeDelegate();
		public int nodeID;
		public Dictionary<string, CreateNodeDelegate> creator;
		public List<Node> nodes;

		private static NodeManager self;
		public static NodeManager Instance {
			get {
				if(null == self)
				{
					self = ScriptableObject.CreateInstance<NodeManager>();
					self.Init();
				}
				return self;
			}
			set {
                NodeManager manager = NodeManager.Clone<NodeManager>(value);
                self = manager;
                for (int i = 0; i < manager.nodes.Count; i++)
                {
                    self.nodes[i] = NodeManager.Clone<Node>(manager.nodes[i]);
                }

                for (int i = 0; i < manager.nodes.Count; i++)
                {
                    Node node = manager.nodes[i];
                    if(null != node.parent)
                    {
                        self.nodes[i].parent = self.FindNode(node.parent.id);
                    }

                    for (int j = 0; j < node.children.Count; j++)
                    {
                        Node child = node.children[j];
                        self.nodes[i].children[j] = self.FindNode(child.id);
                    }
                }
                self.InitCreator();
			}
		}

        private static T Clone<T>(T so) where T : ScriptableObject
        {
            string soName = so.name;
            so = UnityEngine.Object.Instantiate<T>(so);
            so.name = soName;
            return so;
        }

		public void Init()
		{
			nodeID = 0;
			nodes = new List<Node>();
			InitCreator ();
		}

		void InitCreator()
		{
			creator = new Dictionary<string, CreateNodeDelegate>();

			List<Assembly> scriptAssemblies = AppDomain.CurrentDomain.GetAssemblies ().Where ((Assembly assembly) => assembly.FullName.Contains ("Assembly")).ToList ();
			if (!scriptAssemblies.Contains (Assembly.GetExecutingAssembly ())) {
				scriptAssemblies.Add (Assembly.GetExecutingAssembly ());
			}

			foreach (Assembly assembly in scriptAssemblies) 
			{
				foreach (Type type in assembly.GetTypes ().Where (T => T.IsClass && !T.IsAbstract && T.IsSubclassOf (typeof (Node)))) 
				{
					object[] nodeAttributes = type.GetCustomAttributes (typeof (NodeAttribute), false);
					NodeAttribute attr = nodeAttributes [0] as NodeAttribute;
					if (attr != null)
					{
						string typeName = type.Name;
						creator.Add(attr.contextText, () =>
						{
							Node node = ScriptableObject.CreateInstance(typeName) as Node;
							return node;
						});
					}
				}
			}
		}

        public NodeManager Clone()
        {
            NodeManager clone = new NodeManager();
            clone.Init();
            clone.nodeID = self.nodeID;
            foreach(Node node in self.nodes)
            {
                clone.nodes.Add(NodeManager.Clone<Node>(node));
            }

            foreach (Node node in clone.nodes)
            {
                if (null != node.parent)
                {
                    node.parent = clone.FindNode(node.parent.id);
                }

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node child = node.children[i];
                    node.children[i] = clone.FindNode(child.id);
                }
            }
            return clone;
        }

        Node FindNode(int id)
        {
            foreach(Node node in nodes)
            {
                if(node.id == id)
                {
                    return node;
                }
            }
            return null;
        }
	}
}