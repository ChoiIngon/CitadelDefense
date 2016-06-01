﻿using UnityEngine;
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
        private static AIEditorWindow window;
        
        private static string windowTitle = "AI Editor";

        [MenuItem("Window/AI Editor")]
        static void ShowEditor()
        {
            window = EditorWindow.GetWindow<AIEditorWindow>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent(windowTitle);
        }

        public void OnDestory() {}

        Vector2 vanishingPoint = new Vector2(0, 21);

        void OnGUI()
        {
            if (null == window)
            {
                return;
            }
            
            Event e = Event.current;
            mousePos = e.mousePosition;

            bool leftClick = 0 == e.button;
            bool rightClick = 1 == e.button;
            bool mouseDown = EventType.MouseDown == e.type;
            bool mouseUp = EventType.MouseUp == e.type;
			bool mouseDrag = EventType.MouseDrag == e.type;
			bool scrollWheel = EventType.ScrollWheel == e.type;

            if (mouseDown)
            {
                int selectedIndex = GetSelectedIndex();
                if(-1 != selectedIndex)
                {
                    UnityEditor.Selection.activeObject = NodeManager.Instance.nodes[selectedIndex];
                }
            }

            if (rightClick && mouseDown && EditorState.MakeTransition != editorState)
            {
                GenericMenu menu = new GenericMenu();
                int selectedIndex = GetSelectedIndex();

                if (null != NodeManager.Instance.creator)
                {
                    foreach (var v in NodeManager.Instance.creator)
                    {
                        menu.AddItem(new GUIContent("Add " + v.Key), false, CreateNode, v.Key);
                    }
                }

                if (-1 != selectedIndex)
                {
                    menu.AddSeparator("");
                    selectedNode = NodeManager.Instance.nodes[selectedIndex];
                    menu.AddItem(new GUIContent("Make Transition"), false, MakeTransition, null);
                    menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode, null);
                }
                    
                menu.ShowAsContext();
                e.Use();
            }
            else if (leftClick && mouseDown && EditorState.MakeTransition == editorState)
            {
                int selectedIndex = GetSelectedIndex();

                if (-1 != selectedIndex && !NodeManager.Instance.nodes[selectedIndex].Equals(selectedNode))
                {
                    Node node = NodeManager.Instance.nodes[selectedIndex];
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
                    foreach (Node node in NodeManager.Instance.nodes)
                    {
						node.rect.position += e.delta;
					}
					Repaint ();
				}
			}

			if (scrollWheel) {
				zoom = (float)Math.Round (Math.Min (2.0f, Math.Max (0.6f, zoom + e.delta.y / 15)), 2);
				Repaint ();
			}

            Matrix4x4 oldMatrix = GUI.matrix;

            Matrix4x4 matTrans = Matrix4x4.TRS(vanishingPoint, Quaternion.identity, Vector3.one);
            Matrix4x4 matScale = Matrix4x4.Scale(new Vector3(zoom, zoom, 1.0f));
            GUI.matrix = matTrans * matScale * matTrans.inverse;
            if (EditorState.MakeTransition == editorState && null != selectedNode)
            {
                Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
                Node.DrawConnection(selectedNode.rect, mouseRect);
                Repaint();
            }

            for (int i = 0; i < NodeManager.Instance.nodes.Count; i++)
            {
                NodeManager.Instance.nodes[i].DrawConnection();
            }

            BeginWindows();
            for (int i = 0; i < NodeManager.Instance.nodes.Count; i++)
            {
                NodeManager.Instance.nodes[i].rect = GUI.Window(i, NodeManager.Instance.nodes[i].rect, DrawNode, NodeManager.Instance.nodes[i].title);
            }
            EndWindows();

            GUI.matrix = oldMatrix;

            DrawSideGUI();
        }

        void DrawSideGUI()
        {
            GUILayout.BeginArea(new Rect(window.position.width - 200, 0, 200, window.position.height));
            if (GUILayout.Button(new GUIContent("Save Canvas", "Saves the Canvas to a Canvas Save File in the Assets Folder")))
            {
                Save();
            }

            if (GUILayout.Button(new GUIContent("Load Canvas", "Loads the Canvas from a Canvas Save File in the Assets Folder")))
            {
                Load();
            }

            if (GUILayout.Button(new GUIContent("New Canvas", "Loads an empty Canvas")))
            {
            }

            GUILayout.EndArea();
        }
        void DrawNode(int id)
        {
			//NodeManager.nodes [id].rect.position *= zoom;
            NodeManager.Instance.nodes[id].DrawNode();

            GUI.DragWindow();
        }
        void CreateNode(object obj)
        {
            string nodeType = obj.ToString();
            Node node = NodeManager.Instance.creator[nodeType]();
            node.id = NodeManager.Instance.nodeID;
            node.rect.x = mousePos.x;
            node.rect.y = mousePos.y;
			NodeManager.Instance.nodeID++;
            NodeManager.Instance.nodes.Add(node);

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
                selectedNode = NodeManager.Instance.nodes[selectedIndex];
                editorState = EditorState.MakeTransition;
            }
        }
        void DeleteNode(object obj)
        {
            int selectedIndex = GetSelectedIndex();
            if (-1 != selectedIndex)
            {
                selectedNode = NodeManager.Instance.nodes[selectedIndex];
                selectedNode.parent.children.Remove(selectedNode);
                NodeManager.Instance.nodes.RemoveAt(selectedIndex);
            }
        }

        int GetSelectedIndex()
        {
            int selectedIndex = -1;

            for (int i = 0; i < NodeManager.Instance.nodes.Count; i++)
            {
                if (NodeManager.Instance.nodes[i].rect.Contains(mousePos))
                {
                    selectedIndex = i;
                    break;
                }
            }
            return selectedIndex;
        }

        void Save()
        {
            string path = UnityEditor.EditorUtility.SaveFilePanelInProject("Save Node Canvas", "Node Canvas", "asset", "", "Assets/Resources/Saves/");
            AssetDatabase.CreateAsset(NodeManager.Instance, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void Load()
        {
            string path = UnityEditor.EditorUtility.OpenFilePanel("Load Node Canvas", "Assets/Resources/Saves/", "asset");
            path = path.Replace(Application.dataPath, "Assets");
            NodeManager.Instance = AssetDatabase.LoadAssetAtPath(path, typeof(NodeManager)) as NodeManager;
        }
    }

}