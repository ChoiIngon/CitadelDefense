using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AI
{
	[Node("Action/Idle")]
	public class Action_Idle : BehaviourTree.Node
    {
		public Action_Idle()
		{
			title = "Idle";
			rect = new Rect (0, 0, 80, 50);
		}

        public override bool Update()
        {
            return true;
        }
    }

}
