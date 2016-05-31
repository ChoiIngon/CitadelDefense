using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace AIEditor
{
    public class AIEditor : EditorWindow
    {
        public enum EditorState
        {
            Default,
            MakeTransition
        }

        private List<Node> nodes = new List<Node>();
        private Node selectedNode;
        private EditorState editorState = EditorState.Default;
        private Vector2 mousePos;

        [MenuItem("Window/AI Editor")]
        static void ShowEditor()
        {
            AIEditor editor = EditorWindow.GetWindow<AIEditor>();
            editor.InitNode();
        }

        void InitNode()
        {
            NodeCreator.Init();
        }

        void OnGUI()
        {
            Event e = Event.current;
            mousePos = e.mousePosition;

            bool leftClick = 0 == e.button;
            bool rightClick = 1 == e.button;
            bool mouseDown = EventType.MouseDown == e.type;
            bool mouseUp = EventType.MouseUp == e.type;

            if (rightClick && mouseDown && EditorState.MakeTransition != editorState)
            {
                GenericMenu menu = new GenericMenu();
                int selectedIndex = GetSelectedIndex();

                if (null != NodeCreator.creator)
                {
                    foreach (var v in NodeCreator.creator)
                    {
                        menu.AddItem(new GUIContent("Add " + v.Key), false, CreateNode, v.Key);
                    }
                }

                if (-1 != selectedIndex)
                {
                    menu.AddSeparator("");
                    selectedNode = nodes[selectedIndex];
                    menu.AddItem(new GUIContent("Make Transition"), false, MakeTransition, null);
                    menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode, null);
                }
                    
                menu.ShowAsContext();
                e.Use();
            }
            else if (leftClick && mouseDown && EditorState.MakeTransition == editorState)
            {
                int selectedIndex = GetSelectedIndex();
                
                if (-1 != selectedIndex && !nodes[selectedIndex].Equals(selectedNode))
                {
                    Node node = nodes[selectedIndex];
                    node.parent = selectedNode;
                    selectedNode.children.Add(node);
                }

                editorState = EditorState.Default;
                selectedNode = null;

                e.Use();
            }
            
            if (EditorState.MakeTransition == editorState && null != selectedNode)
            {
                Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
                Node.DrawConnection(selectedNode.rect, mouseRect);
                Repaint();
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].DrawConnection();
            }

            BeginWindows();
            for(int i=0; i<nodes.Count; i++)
            {
                nodes[i].rect = GUI.Window(i, nodes[i].rect, DrawNode, nodes[i].title);
            }
            EndWindows();
        }

        void DrawNode(int id)
        {
            nodes[id].DrawNode();
            GUI.DragWindow();
        }
        void CreateNode(object obj)
        {
            string nodeType = obj.ToString();
            Node node = NodeCreator.CreateInstance(nodeType);
            node.rect.x = mousePos.x;
            node.rect.y = mousePos.y;
            nodes.Add(node);

            if(null != selectedNode)
            {
                selectedNode.children.Add(node);
                node.parent = selectedNode;
                selectedNode = null;
            }
        }
        void MakeTransition(object obj)
        {
            int selectedIndex = GetSelectedIndex();
            if (-1 != selectedIndex)
            {
                selectedNode = nodes[selectedIndex];
                editorState = EditorState.MakeTransition;
            }
        }
        void DeleteNode(object obj)
        {
            int selectedIndex = GetSelectedIndex();
            if (-1 != selectedIndex)
            {
                selectedNode = nodes[selectedIndex];
                selectedNode.parent.children.Remove(selectedNode);
                nodes.RemoveAt(selectedIndex);
            }
        }

        int GetSelectedIndex()
        {
            int selectedIndex = -1;

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].rect.Contains(mousePos))
                {
                    selectedIndex = i;
                    break;
                }
            }
            return selectedIndex;
        }
    }

}