using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Attack")]
	public class Action_Attack : Node {
		public Action_Attack()
		{
			title = "Attack";
			rect = new Rect (0, 0, 80, 50);
		}
		public override void DrawNode()
		{
		}
        public override string ToString()
        {
            return
                "{" +
					"\"type\":" + "\"Action_Attack\"" + "," +
                    "\"id\":" + id +
                "}";
        }
	}

}
