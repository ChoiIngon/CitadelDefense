using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AIEditor
{
    public class AIEditorWindow : EditorWindow
    {
        public enum EditorState
        {
            Default,
            MakeTransition
        }

        private Node selectedNode;
        private EditorState editorState = EditorState.Default;
        private Vector2 mousePos;
		private float zoom = 1.0f;

        [MenuItem("Window/AI Editor")]
        static void ShowEditor()
        {
            AIEditorWindow window = EditorWindow.GetWindow<AIEditorWindow>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("AI Editor");
        }

        public void OnDestory() {}

        void OnGUI()
        {
            if (false == NodeManager.isInit)
            {
                NodeManager.Init();
            }

            Event e = Event.current;
            mousePos = e.mousePosition;

            bool leftClick = 0 == e.button;
            bool rightClick = 1 == e.button;
            bool mouseDown = EventType.MouseDown == e.type;
            bool mouseUp = EventType.MouseUp == e.type;
			bool mouseDrag = EventType.MouseDrag == e.type;
			bool scrollWheel = EventType.ScrollWheel == e.type;
            if (rightClick && mouseDown && EditorState.MakeTransition != editorState)
            {
                GenericMenu menu = new GenericMenu();
                int selectedIndex = GetSelectedIndex();

                if (null != NodeManager.creator)
                {
                    foreach (var v in NodeManager.creator)
                    {
                        menu.AddItem(new GUIContent("Add " + v.Key), false, CreateNode, v.Key);
                    }
                }

                if (-1 != selectedIndex)
                {
                    menu.AddSeparator("");
                    selectedNode = NodeManager.nodes[selectedIndex];
                    menu.AddItem(new GUIContent("Make Transition"), false, MakeTransition, null);
                    menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode, null);
                }
                    
                menu.ShowAsContext();
                e.Use();
            }
            else if (leftClick && mouseDown && EditorState.MakeTransition == editorState)
            {
                int selectedIndex = GetSelectedIndex();

                if (-1 != selectedIndex && !NodeManager.nodes[selectedIndex].Equals(selectedNode))
                {
                    Node node = NodeManager.nodes[selectedIndex];
                    node.parent = selectedNode;
                    selectedNode.children.Add(node);
                }

                editorState = EditorState.Default;
                selectedNode = null;

                e.Use();
            }
			if (mouseDrag) {
				int selectedIndex = GetSelectedIndex ();
				if (-1 == selectedIndex) {
					foreach (Node node in NodeManager.nodes) {
						node.rect.position += e.delta;
					}
					Repaint ();
				}
			}

			if (scrollWheel) {
				zoom = (float)Math.Round (Math.Min (2.0f, Math.Max (0.6f, zoom + e.delta.y / 15)), 2);
				Repaint ();
			}

            if (EditorState.MakeTransition == editorState && null != selectedNode)
            {
                Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
                Node.DrawConnection(selectedNode.rect, mouseRect);
                Repaint();
            }

            for (int i = 0; i < NodeManager.nodes.Count; i++)
            {
                NodeManager.nodes[i].DrawConnection();
            }

            BeginWindows();
            for (int i = 0; i < NodeManager.nodes.Count; i++)
            {
                NodeManager.nodes[i].rect = GUI.Window(i, NodeManager.nodes[i].rect, DrawNode, NodeManager.nodes[i].title);
            }
            EndWindows();
        }

        void DrawNode(int id)
        {
			//NodeManager.nodes [id].rect.position *= zoom;
            NodeManager.nodes[id].DrawNode();

            GUI.DragWindow();
        }
        void CreateNode(object obj)
        {
            string nodeType = obj.ToString();
            Node node = NodeManager.creator[nodeType]();
			node.id = NodeManager.nodeID;
            node.rect.x = mousePos.x;
            node.rect.y = mousePos.y;
			NodeManager.nodeID++;
            NodeManager.nodes.Add(node);

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
                selectedNode = NodeManager.nodes[selectedIndex];
                editorState = EditorState.MakeTransition;
            }
        }
        void DeleteNode(object obj)
        {
            int selectedIndex = GetSelectedIndex();
            if (-1 != selectedIndex)
            {
                selectedNode = NodeManager.nodes[selectedIndex];
                selectedNode.parent.children.Remove(selectedNode);
                NodeManager.nodes.RemoveAt(selectedIndex);
            }
        }

        int GetSelectedIndex()
        {
            int selectedIndex = -1;

            for (int i = 0; i < NodeManager.nodes.Count; i++)
            {
                if (NodeManager.nodes[i].rect.Contains(mousePos))
                {
                    selectedIndex = i;
                    break;
                }
            }
            return selectedIndex;
        }
    }

}