using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
using System.Linq;
#endif

namespace AI
{
    public class NodeManager : ScriptableObject
    {
        public int nodeID;
        public delegate BehaviourTree.Node CreateNodeDelegate();
        public Dictionary<string, CreateNodeDelegate> creator;
        public BehaviourTree.Node root
        {
            get
            {
                if(1 > nodes.Count)
                {
                    return null;
                }
                BehaviourTree.Node node = nodes[0];
                while(null != node.parent)
                {
                    node = node.parent;
                }
                return node;
            }
        }
        public List<BehaviourTree.Node> nodes;

        
        public static T Clone<T>(T so) where T : ScriptableObject
        {
            string soName = so.name;
            so = Object.Instantiate<T>(so);
            so.name = soName;
            return so;
        }

        public void Init()
        {
            nodeID = 0;
            nodes = new List<BehaviourTree.Node>();
            InitCreator();
        }

        void InitCreator()
        {
#if UNITY_EDITOR
            creator = new Dictionary<string, CreateNodeDelegate>();

            List<Assembly> scriptAssemblies = System.AppDomain.CurrentDomain.GetAssemblies().Where((Assembly assembly) => assembly.FullName.Contains("Assembly")).ToList();
            if (!scriptAssemblies.Contains(Assembly.GetExecutingAssembly()))
            {
                scriptAssemblies.Add(Assembly.GetExecutingAssembly());
            }

            foreach (Assembly assembly in scriptAssemblies)
            {
                foreach (System.Type type in assembly.GetTypes().Where(T => T.IsClass && !T.IsAbstract && T.IsSubclassOf(typeof(BehaviourTree.Node))))
                {
                    object[] nodeAttributes = type.GetCustomAttributes(typeof(NodeAttribute), false);
                    if(0 == nodeAttributes.Length)
                    {
                        continue;
                    }
                    NodeAttribute attr = nodeAttributes[0] as NodeAttribute;
                    if (attr != null)
                    {
                        string typeName = type.Name;
                        creator.Add(attr.contextText, () =>
                        {
                            BehaviourTree.Node node = ScriptableObject.CreateInstance(typeName) as BehaviourTree.Node;
                            return node;
                        });
                    }
                }
            }
#endif
        }

        public NodeManager Clone()
        {
            NodeManager clone = ScriptableObject.CreateInstance<NodeManager>();
            clone.Init();
            clone.nodeID = nodeID;
            foreach (BehaviourTree.Node node in nodes)
            {
                clone.nodes.Add(NodeManager.Clone<BehaviourTree.Node>(node));
            }

            foreach (BehaviourTree.Node node in clone.nodes)
            {
                if (null != node.parent)
                {
                    node.parent = clone.FindNode(node.parent.id);
                }

                for (int i = 0; i < node.children.Count; i++)
                {
                    BehaviourTree.Node child = node.children[i];
                    node.children[i] = clone.FindNode(child.id);
                }
            }
            return clone;
        }

        BehaviourTree.Node FindNode(int id)
        {
            foreach (BehaviourTree.Node node in nodes)
            {
                if (node.id == id)
                {
                    return node;
                }
            }
            return null;
        }
    }
}