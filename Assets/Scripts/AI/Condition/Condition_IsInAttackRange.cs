using UnityEngine;
using System.Collections;

namespace AI
{
	[Node("Condition/IsInAttackRange")]
	public class Condition_IsInAttackRange : BehaviourTree.Node
    {
		public Condition_IsInAttackRange()
		{
			title = "IsInAttackRange";
			rect = new Rect (0, 0, 150, 50);
		}

        public override bool Update()
        {
            return true;
        }
    }

}
