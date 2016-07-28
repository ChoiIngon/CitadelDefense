using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace AI
{
	[Node("Condition/IsInSightRange")]
	public class Condition_IsInSightRange : BehaviourTree.Node
    {
        public Condition_IsInSightRange()
		{
            title = "IsInSightRange";
			rect = new Rect (0, 0, 150, 50);
		}

        public override bool Update()
        {
            return true;
        }
    }

}
