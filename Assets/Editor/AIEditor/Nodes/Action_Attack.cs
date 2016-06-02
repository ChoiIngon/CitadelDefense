using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor 
{
	[Node("Action/Attack")]
	public class Action_Attack : Node {
        public RuntimeAnimatorController animator;
		public Action_Attack()
		{
			title = "Attack";
			rect = new Rect (0, 0, 180, 150);
		}
		public override void DrawNode()
		{
            EditorGUILayout.LabelField("Animator");
            animator = (RuntimeAnimatorController)EditorGUILayout.ObjectField(animator, typeof(RuntimeAnimatorController));
		}
        public override string ToString()
        {
            return
                "{" +
					"\"type\":" + "\"Action_Attack\"" + "," +
                    "\"id\":" + id +
                "}";
        }
	}

}
