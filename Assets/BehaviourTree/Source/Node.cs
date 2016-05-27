using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Node : ScriptableObject {
    public Rect rect = new Rect();
    public Vector2 contentOffset = Vector2.zero;

    protected void Init()
    {
        if(false == NodeEditor.curNodeCanvas.nodes.Contains(this))
        {
            NodeEditor.curNodeCanvas.nodes.Add(this);
        }
#if UNITY_EDITOR
        if("" == name)
        {
            name = UnityEditor.ObjectNames.NicifyVariableName(id);
        }
#endif
    }

    public void Delete()
    {
        if(false == NodeEditor.curNodeCanvas.nodes.Contains(this))
        {
            throw new UnityException("The Node " + name + " does not exist on the Canvas " + NodeEditor.curNodeCanvas.name + "!");
        }

        //NodeEditorCallbacks.IssueOnDeleteNode(this);
        NodeEditor.curNodeCanvas.nodes.Remove(this);

        DestroyImmediate(this, true);
    }


    protected virtual void DrawNode()
    {
        Rect nodeRect = rect;
        nodeRect.position += NodeEditor.curEditorState.zoomPanAdjust;
        contentOffset = new Vector2(0, 20);

        Rect headerRect = new Rect(nodeRect.x, nodeRect.y, nodeRect.width, contentOffset.y);
        //GUI.Label(headerRect, name, NodeEditor.curEditorState.selectedNode == this ? Node)
    }

    public abstract string id { get; }
    public abstract Node Create(Vector2 pos);
    protected abstract void NodeGUI();
}
