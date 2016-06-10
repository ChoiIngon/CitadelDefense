using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor
{
	[Node("Decorator/Selector")]
    public class Selector : Node
    {
		public Selector()
		{
			title = "Selector";
			rect = new Rect (0, 0, 100, 50);
		}
        public override void DrawNode()
        {
        }

        public override string ToString()
        {
            return
                "{" +
                    "\"type\":" + "\"Selector\"" + "," +
                    "\"id\":" + id +
                "}";
        }
    }
}