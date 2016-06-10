using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Patrol")]
	public class Action_Patrol : Node {
        public Action_Patrol()
		{
			title = "Patrol";
			rect = new Rect (0, 0, 180, 150);
		}
		public override void DrawNode()
		{
            
		}
        public override string ToString()
        {
            return
                "{" +
                    "\"type\":" + "\"Action_Patrol\"" + "," +
                    "\"id\":" + id +
                "}";
        }
	}

}
