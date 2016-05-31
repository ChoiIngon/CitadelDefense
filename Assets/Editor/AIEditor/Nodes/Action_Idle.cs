using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Idle")]
	public class Action_Idle : Node {
		public string animName;
		public Action_Idle()
		{
			title = "Idle";
			rect = new Rect (0, 0, 200, 50);
		}
		public override void DrawNode()
		{
			animName = EditorGUILayout.TextField ("animation name", animName);
		}
	}

}
