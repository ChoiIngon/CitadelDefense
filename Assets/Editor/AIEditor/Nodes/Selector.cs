using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor
{
	[Node("Common/Selector")]
    public class Selector : Node
    {
        public string windowTitle = "";
		public Selector()
		{
			title = "Selector";
			rect = new Rect (0, 0, 100, 50);
		}
        public override void DrawNode()
        {
            windowTitle = EditorGUILayout.TextField("Title", windowTitle);
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