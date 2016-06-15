using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Move")]
	public class Action_Move : Node {
		public Action_Move()
		{
			title = "Move";
			rect = new Rect (0, 0, 100, 50);
		}
		public override void DrawNode()
		{
		}
        public override string ToString()
        {
            return
                "{" +
					"\"type\":" + "\"Action_Move\"" + "," +
                    "\"id\":" + id + "," +
                    "\"parent\":" + (null != parent ? parent.id.ToString() : "\"\"") +
                "}";
        }
	}

}
