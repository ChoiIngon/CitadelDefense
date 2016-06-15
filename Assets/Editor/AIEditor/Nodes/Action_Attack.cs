using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Attack")]
	public class Action_Attack : Node {
        public AnimationClip animationClip;
		public Action_Attack()
		{
			title = "Attack";
			rect = new Rect (0, 0, 180, 150);
		}
		public override void DrawNode()
		{
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Animation Clip", GUILayout.Width(90));
            animationClip = (AnimationClip)EditorGUILayout.ObjectField(animationClip, typeof(AnimationClip), GUILayout.Width(130));
            EditorGUILayout.EndHorizontal();
		}
        public override string ToString()
        {
            return
                "{" +
					"\"type\":" + "\"Action_Attack\"" + "," +
                    "\"id\":" + id + "," +
                    "\"parent\":" + (null != parent ? parent.id.ToString() : "\"\"") +
                "}";
        }
	}

}
