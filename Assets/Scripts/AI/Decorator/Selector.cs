using UnityEngine;
using System.Collections;

namespace AI
{ 
    [Node("Decorator/Selector")]
    public class Selector : BehaviourTree.Node
    {
        public Selector()
        {
            title = "Selector";
            rect = new Rect(0, 0, 100, 50);
        }

        public override bool Update()
        {
            foreach (BehaviourTree.Node child in children)
            {
                if (true == child.Update())
                {
                    return true;
                }
            }
            return false;
        }
    }
}