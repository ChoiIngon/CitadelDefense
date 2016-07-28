using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    public class AIEditorWindow : EditorWindow
    {
        public enum EditorState
        {
            Default,
            MakeTransition
        }

        private const string defaultAssetPath = "Assets/Resources/AI";
        private BehaviourTree.Node selectedNode;
        private EditorState editorState = EditorState.Default;
        private Vector2 mousePos;
		private float zoom = 1.0f;
        private static AIEditorWindow window;
        private static string windowTitle = "AI Editor";
        private static NodeManager manager;

        [MenuItem("Window/AI Editor")]
        static void ShowEditor()
        {
            window = EditorWindow.GetWindow<AIEditorWindow>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent(windowTitle);

            manager = ScriptableObject.CreateInstance<NodeManager>();
            manager.Init();
        }

        [UnityEditor.Callbacks.OnOpenAsset(1)]
        public static bool AutoOpen(int instanceID, int line)
        {
            if (Selection.activeObject != null && Selection.activeObject.GetType() == typeof(NodeManager))
            {
                string path = AssetDatabase.GetAssetPath(instanceID);
                AIEditorWindow.ShowEditor();
                window.Load(path);
                return true;
            }
            return false;
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
                    UnityEditor.Selection.activeObject = AIEditorWindow.manager.nodes[selectedIndex];
                }
            }

            if (rightClick && mouseDown && EditorState.MakeTransition != editorState)
            {
                GenericMenu menu = new GenericMenu();
                int selectedIndex = GetSelectedIndex();

                if (null != AIEditorWindow.manager.creator)
                {
                    foreach (var v in AIEditorWindow.manager.creator)
                    {
                        menu.AddItem(new GUIContent("Add " + v.Key), false, CreateNode, v.Key);
                    }
                }

                if (-1 != selectedIndex)
                {
                    menu.AddSeparator("");
                    selectedNode = AIEditorWindow.manager.nodes[selectedIndex];
                    menu.AddItem(new GUIContent("Make Transition"), false, MakeTransition, null);
                    menu.AddItem(new GUIContent("Delete Node"), false, DeleteNode, null);
                }
                    
                menu.ShowAsContext();
                e.Use();
            }
            else if (leftClick && mouseDown && EditorState.MakeTransition == editorState)
            {
                int selectedIndex = GetSelectedIndex();

                if (-1 != selectedIndex && !AIEditorWindow.manager.nodes[selectedIndex].Equals(selectedNode))
                {
                    BehaviourTree.Node node = AIEditorWindow.manager.nodes[selectedIndex];
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
					foreach (BehaviourTree.Node node in AIEditorWindow.manager.nodes) {
                        node.rect.position += e.delta;
					}
					Repaint ();
				} else {
                    BehaviourTree.Node node = AIEditorWindow.manager.nodes [selectedIndex];
                    BehaviourTree.Node parent = node.parent;
					if (null != parent) {
						parent.children.Sort ((BehaviourTree.Node lhs, BehaviourTree.Node rhs) => {
							if(lhs.rect.position.x < rhs.rect.position.x)
							{
								return -1;
							}
							return 1;
						});
					}
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
                BehaviourTree.Node.DrawConnection(selectedNode.rect, mouseRect);
                Repaint();
            }

            for (int i = 0; i < AIEditorWindow.manager.nodes.Count; i++)
            {
                AIEditorWindow.manager.nodes[i].DrawConnection();
            }

            BeginWindows(); 
            for (int i = 0; i < AIEditorWindow.manager.nodes.Count; i++)
            {
                AIEditorWindow.manager.nodes[i].rect = GUI.Window(i, AIEditorWindow.manager.nodes[i].rect, DrawNode, AIEditorWindow.manager.nodes[i].title);
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
                string path = UnityEditor.EditorUtility.SaveFilePanelInProject("Save AI Asset", "AIBehaviourTree", "asset", "", defaultAssetPath);
                if ("" == path)
                {
                    return;
                }
            
                Save(path);
            }

            if (GUILayout.Button(new GUIContent("Load Canvas", "Loads the Canvas from a Canvas Save File in the Assets Folder")))
            {
                string path = UnityEditor.EditorUtility.OpenFilePanel("Load AI Asset", defaultAssetPath, "asset");
                path = path.Replace(Application.dataPath, "Assets");
            
                Load(path);
            }

            if (GUILayout.Button(new GUIContent("New Canvas", "Loads an empty Canvas")))
            {
                AIEditorWindow.manager.Init();
            }

            GUILayout.EndArea();
        }
        void DrawNode(int id)
        {
            //NodeManager.nodes [id].rect.position *= zoom;
            AIEditorWindow.manager.nodes[id].DrawNode();
            GUI.DragWindow();
        }

        void CreateNode(object obj)
        {
            string nodeType = obj.ToString();
            BehaviourTree.Node node = AIEditorWindow.manager.creator[nodeType]();
            node.id = AIEditorWindow.manager.nodeID++;
            node.rect.x = mousePos.x;
            node.rect.y = mousePos.y;
            AIEditorWindow.manager.nodes.Add(node);

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
                selectedNode = AIEditorWindow.manager.nodes[selectedIndex];
                editorState = EditorState.MakeTransition;
            }
        }
        void DeleteNode(object obj)
        {
            int selectedIndex = GetSelectedIndex();
            if (-1 != selectedIndex)
            {
                selectedNode = AIEditorWindow.manager.nodes[selectedIndex];
                if (null != selectedNode.parent)
                {
                    selectedNode.parent.children.Remove(selectedNode);
                }
                AIEditorWindow.manager.nodes.RemoveAt(selectedIndex);
            }
        }

        int GetSelectedIndex()
        {
            int selectedIndex = -1;

            for (int i = 0; i < AIEditorWindow.manager.nodes.Count; i++)
            {
                if (AIEditorWindow.manager.nodes[i].rect.Contains(mousePos))
                {
                    selectedIndex = i;
                    break;
                }
            }
            return selectedIndex;
        }

        void Save(string path)
        {
            string assetName = System.IO.Path.GetFileName(path);
            assetName = assetName.Replace(".asset", "");

            NodeManager clone = AIEditorWindow.manager.Clone();
            clone.name = assetName;
            
			AssetDatabase.CreateAsset(clone, path);
			foreach (BehaviourTree.Node node in clone.nodes) {
                UnityEditor.AssetDatabase.AddObjectToAsset(node, clone);
				node.hideFlags = HideFlags.HideInHierarchy;
			}

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void Load(string path)
        {
            NodeManager tmp = AssetDatabase.LoadAssetAtPath(path, typeof(NodeManager)) as NodeManager;
            AIEditorWindow.manager = tmp.Clone();
        }
    }
}