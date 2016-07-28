using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace AI
{
	[Node("Action/Approach")]
	public class Action_Approach : BehaviourTree.Node {
        public Action_Approach()
		{
            title = "Approch";
			rect = new Rect (0, 0, 180, 150);
        }

        public override void DrawNode()
		{
		}
        
        public override bool Update()
        {
            return true;
        }
    }

}
