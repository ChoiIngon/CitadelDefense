using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Condition/IsInSightRange")]
	public class Condition_IsInSightRange : Node {
        public Condition_IsInSightRange()
		{
            title = "IsInSightRange";
			rect = new Rect (0, 0, 150, 50);
		}
		public override void DrawNode()
		{
		}
        public override string ToString()
        {
            return
                "{" +
                    "\"type\":" + "\"Condition_IsInSightRange\"" + "," +
                    "\"id\":" + id + "," +
                    "\"parent\":" + (null != parent ? parent.id.ToString() : "\"\"") +
                "}";
        }
	}

}
