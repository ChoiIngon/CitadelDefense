using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AI
{
	[Node("Action/Move")]
	public class Action_Move : BehaviourTree.Node {
		public Action_Move()
		{
            title = "Move";
			rect = new Rect (0, 0, 100, 50);
        }
        
        public override bool Update()
        {
            return true;
        }
    }

}
