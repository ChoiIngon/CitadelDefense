using UnityEngine;
using UnityEditor;
using System.Collections;

namespace BehaviourTree
{
    public class WindowStatus : ScriptableObject
    {
        public bool drawing = true;
        public Node selectedNode;
        public Node focusedNode;

        public bool dragNode;
        public Node makeTransition;
        public float zoom = 1.0f;
    }

    public class BehaviourTreeWindow : EditorWindow
    {
        public static BehaviourTreeWindow window;

        public static int sideWindowWidth = 400;
        public Rect sideWindowRect { get { return new Rect(position.width - sideWindowWidth, 0, sideWindowWidth, position.height); } }
        public Rect canvasWindowRect { get { return new Rect(0, 0, position.width - sideWindowWidth, position.height); } }


        [MenuItem("Window/Behaviour Tree")]
        public static void Create()
        {
            window = GetWindow<BehaviourTreeWindow>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("Behaviour Tree Editor");

            ResourceManager.defaultResourcePath = "Assets/Node_Editor/Resources/";
        }

        private void InitGUI()
        {
            NodeEditor.Init();
        }

        private void OnGUI()
        {
            if (null == NodeEditor.nodeSkin)
            {
                InitGUI();
            }

            NodeEditor.InputEvent();
            DrawBackground();

            GUILayout.BeginArea(sideWindowRect, GUI.skin.box);

			NodeEditor.defaultSkin = GUI.skin;
			GUI.skin = NodeEditor.nodeSkin;

            foreach (Node node in NodeEditor.nodes)
            {
                node.DrawNode();
            }
            DrawSideWindow();
            
			GUI.skin = NodeEditor.defaultSkin;
            GUILayout.EndArea();
        }

        private void DrawBackground()
        {
            if(EventType.Repaint == Event.current.type)
            {
                GUI.BeginClip(canvasWindowRect);
                float width = NodeEditor.background.width;
                float height = NodeEditor.background.height;
                Vector2 offset = new Vector2(width, height);
                int tileX = Mathf.CeilToInt((canvasWindowRect.width + (width - offset.x)) / width);
                int tileY = Mathf.CeilToInt((canvasWindowRect.height + (height - offset.y)) / height);

                for (int x = 0; x < tileX; x++)
                {
                    for (int y = 0; y < tileY; y++)
                    {
                        GUI.DrawTexture(new Rect(x * width, y * height, width, height), NodeEditor.background);
                    }
                }
                GUI.EndClip();
            }
        }

        private void DrawSideWindow()
        {
            // GUILayout.Label(new GUIContent("Node Editor (" + mainNodeCanvas.name + ")", "Opened Canvas path: " + openedCanvasPath), NodeEditorGUI.nodeLabelBold);

            if (GUILayout.Button(new GUIContent("Save Canvas", "Saves the Canvas to a Canvas Save File in the Assets Folder")))
            {
                /*
                string path = EditorUtility.SaveFilePanelInProject("Save Node Canvas", "Node Canvas", "asset", "", NodeEditor.editorPath + "Resources/Saves/");
                if (!string.IsNullOrEmpty(path))
                    SaveNodeCanvas(path);
                */
            }

            if (GUILayout.Button(new GUIContent("Load Canvas", "Loads the Canvas from a Canvas Save File in the Assets Folder")))
            {
                /*
                string path = EditorUtility.OpenFilePanel("Load Node Canvas", NodeEditor.editorPath + "Resources/Saves/", "asset");
                if (!path.Contains(Application.dataPath))
                {
                    if (!string.IsNullOrEmpty(path))
                        ShowNotification(new GUIContent("You should select an asset inside your project folder!"));
                }
                else
                {
                    path = path.Replace(Application.dataPath, "Assets");
                    LoadNodeCanvas(path);
                }
                */
            }

            if (GUILayout.Button(new GUIContent("New Canvas", "Loads an empty Canvas")))
            {
                //NewNodeCanvas();
            }
            if (GUILayout.Button(new GUIContent("Recalculate All", "Initiates complete recalculate. Usually does not need to be triggered manually.")))
            {
                //NodeEditor.RecalculateAll(mainNodeCanvas);
            }
            if (GUILayout.Button("Force Re-Init"))
            {
                //NodeEditor.ReInit(true);
            }

            /*
            NodeEditorGUI.knobSize = EditorGUILayout.IntSlider(new GUIContent("Handle Size", "The size of the Node Input/Output handles"), NodeEditorGUI.knobSize, 12, 20);
            mainEditorState.zoom = EditorGUILayout.Slider(new GUIContent("Zoom", "Use the Mousewheel. Seriously."), mainEditorState.zoom, 0.6f, 2);

            if (mainEditorState.selectedNode != null)
                if (Event.current.type != EventType.Ignore)
                    mainEditorState.selectedNode.DrawNodePropertyEditor();
            */
        }

        
    }
}