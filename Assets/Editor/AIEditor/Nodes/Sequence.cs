using UnityEngine;
using System.Collections;

namespace AIEditor
{
	[Node("Composite/Sequence")]
    public class Sequence : Node {
		public Sequence()
		{
			title = "Sequence";
			rect = new Rect (0, 0, 100, 50);
		}
        public override void DrawNode()
        {
        }

        public override string ToString()
        {
            return 
                "{" +
                    "\"type\":" + "\"Sequence\"" + "," +
                    "\"id\":" + id +
                "}";
        }
    }
}