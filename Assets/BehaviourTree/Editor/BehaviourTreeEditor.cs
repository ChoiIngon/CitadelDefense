using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;

public class BehaviourTreeEditor : EditorWindow {
    private static BehaviourTreeEditor editor;

    [MenuItem ("Window/BehaviourTree Editor")]
    public static void Create()
    {
        editor = GetWindow<BehaviourTreeEditor>();
        editor.minSize = new Vector2(800, 600);

    }
}
