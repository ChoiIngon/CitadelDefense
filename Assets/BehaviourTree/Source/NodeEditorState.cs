using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeEditorState : ScriptableObject {
    public NodeCanvas canvas;
    public NodeEditorState parentEditor;

    public bool drawing = true;

    public Node selectedNode;
    [System.NonSerialized]
    public Node focusedNode;

    [System.NonSerialized]
    public bool dragNode = false;
    [System.NonSerialized]
    public Node makeTransition;

    public Vector2 panOffset = Vector2.zero;
    public float zoom = 1.0f;

    [System.NonSerialized]
    public bool navigate = false;
    [System.NonSerialized]
    public bool panWindow = false;
    [System.NonSerialized]
    public Rect canvasRect;
    public Vector2 zoomPos { get { return canvasRect.size / 2; } }
    [System.NonSerialized]
    public Vector2 zoomPanAdjust;
    [System.NonSerialized]
    public List<Rect> ignoreInput = new List<Rect>();
}
