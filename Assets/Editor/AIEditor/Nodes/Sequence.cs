using UnityEngine;
using System.Collections;

namespace AIEditor
{
	[Node("Common/Sequence")]
    public class Sequence : Node {
		public Sequence()
		{
			title = "Sequence";
			rect = new Rect (0, 0, 150, 100);
		}
        public override void DrawNode()
        {
        }
    }
}