using UnityEngine;
using UnityEditor;
using System.Collections;

namespace AIEditor
{
    public class Selector : Node
    {
        public string windowTitle = "";
        public override void DrawNode()
        {
            windowTitle = EditorGUILayout.TextField("Title", windowTitle);
        }
    }
}