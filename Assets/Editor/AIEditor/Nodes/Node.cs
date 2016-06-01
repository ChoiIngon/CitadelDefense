using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace AIEditor
{
    public abstract class Node : ScriptableObject
    {
        [HideInInspector]
		public int id;
        [HideInInspector]
        public Node parent;
        [HideInInspector]
        public List<Node> children = new List<Node>();

        public string title;
        public Rect rect;

        public abstract void DrawNode();
        public abstract string ToString();

        public void DrawConnection()
        {
            foreach(Node child in children)
            {
                DrawConnection(rect, child.rect);
            }
        }

        public static void DrawConnection(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);
            Vector3 startTan = startPos + Vector3.up * 50;
            Vector3 endTan = endPos + Vector3.down * 50;
            Color shadowCol = new Color(0, 0, 0, .06f);

            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
            }

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
        }
    }

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
                    self = new NodeManager();
                    self.Init();
                }
                return self;
            }
            private set {}
        }
        void Init()
        {
            nodeID = 0;
            nodes = new List<Node>();
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
                        Instance.creator.Add(attr.contextText, () =>
                        {
							Node node = ScriptableObject.CreateInstance(typeName) as Node;
							return node;
						});
					}
				}
			}
        }
    }

	public class NodeAttribute : Attribute 
	{
		public string contextText { get; private set; }

		public NodeAttribute (string ReplacedContextText) 
		{
			contextText = ReplacedContextText;
		}
	}
}