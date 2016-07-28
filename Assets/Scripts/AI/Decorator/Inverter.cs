using UnityEngine;
using System.Collections;

namespace AI
{
    [Node("Decorator/Inverter")]
    public class Inverter : BehaviourTree.Node
    {
        public Inverter()
        {
            title = "Inverter";
            rect = new Rect(0, 0, 100, 50);
        }

        public override bool Update()
        {
            return !GetChild().Update();
        }
    }
}