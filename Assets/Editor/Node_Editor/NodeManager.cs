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
				self = value; 
				self.InitCreator ();
			}
		}
		void Init()
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
}