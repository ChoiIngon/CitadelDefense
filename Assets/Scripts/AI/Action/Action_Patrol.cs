using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AI
{
	[Node("Action/Patrol")]
	public class Action_Patrol : BehaviourTree.Node
    {
        public Action_Patrol()
		{
			title = "Patrol";
			rect = new Rect (0, 0, 180, 150);
		}

        public override bool Update()
        {
            return true;
        }
    }

}
