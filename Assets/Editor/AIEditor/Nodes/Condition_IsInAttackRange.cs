using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Condition/IsInAttackRange")]
	public class Condition_IsInAttackRange : Node {
		public Condition_IsInAttackRange()
		{
			title = "IsInAttackRange";
			rect = new Rect (0, 0, 150, 50);
		}
		public override void DrawNode()
		{
		}
        public override string ToString()
        {
            return
                "{" +
					"\"type\":" + "\"Condition_IsInAttackRange\"" + "," +
                    "\"id\":" + id +
                "}";
        }
	}

}
