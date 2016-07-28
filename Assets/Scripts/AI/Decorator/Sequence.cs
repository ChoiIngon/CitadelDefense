using UnityEngine;
using System.Collections;

namespace AI
{
    [Node("Decorator/Sequence")]
    public class Sequence : BehaviourTree.Node
    {
        public Sequence()
        {
            title = "Sequence";
            rect = new Rect(0, 0, 100, 50);
        }

        public override bool Update()
        {
            foreach (BehaviourTree.Node child in children)
            {
                if (false == child.Update())
                {
                    return false;
                }
            }
            return true;
        }
    }
}