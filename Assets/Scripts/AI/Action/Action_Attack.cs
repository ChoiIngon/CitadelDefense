using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace AI
{
	[Node("Action/Attack")]
	public class Action_Attack : BehaviourTree.Node
    {
        public AnimationClip animationClip;
		public Action_Attack()
		{
			title = "Attack";
			rect = new Rect (0, 0, 180, 150);
		}
		public override void DrawNode()
		{
#if UNITY_EDITOR
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Animation Clip", GUILayout.Width(90));
            animationClip = (AnimationClip)EditorGUILayout.ObjectField(animationClip, typeof(AnimationClip), GUILayout.Width(130));
            EditorGUILayout.EndHorizontal();
#endif
        }
        public override bool Update()
        {
            return true;
        }
    }

}
