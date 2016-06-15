using UnityEngine;
using System.Collections;

namespace AIEditor
{
    [Node("Decorator/Inverter")]
    public class Inverter : Node
    {
		public Inverter()
		{
            title = "Inverter";
			rect = new Rect (0, 0, 100, 50);
		}
        public override void DrawNode()
        {
        }

        public override string ToString()
        {
            return 
                "{" +
                    "\"type\":" + "\"Inverter\"" + "," +
                    "\"id\":" + id + "," +
                    "\"parent\":" + (null != parent ? parent.id.ToString() : "\"\"") +
                "}";
        }
    }
}