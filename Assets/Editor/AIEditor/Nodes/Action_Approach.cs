using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Approach")]
	public class Action_Approach : Node {
        public Action_Approach()
		{
			title = "Approch";
			rect = new Rect (0, 0, 180, 150);
		}
		public override void DrawNode()
		{
            
		}
        public override string ToString()
        {
            return
                "{" +
                    "\"type\":" + "\"Action_Approch\"" + "," +
                    "\"id\":" + id + "," +
                    "\"parent\":" + (null != parent ? parent.id.ToString() : "\"\"") +
                "}";
        }
	}

}
