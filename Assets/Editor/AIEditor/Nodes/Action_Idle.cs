using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Idle")]
	public class Action_Idle : Node {
		public Action_Idle()
		{
			title = "Idle";
			rect = new Rect (0, 0, 80, 50);
		}
		public override void DrawNode()
		{
		}
        public override string ToString()
        {
            return
                "{" +
                    "\"type\":" + "\"Action_Idle\"" + "," +
                    "\"id\":" + id + "," +
                    "\"parent\":" + (null != parent ? parent.id.ToString() : "\"\"") +
                "}";
        }
	}

}
