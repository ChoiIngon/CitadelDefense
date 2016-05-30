using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public static class NodeEditor {
        public static Color textColor = new Color(0.7f, 0.7f, 0.7f);

        public static Texture2D background;
        public static Texture2D guiBox;
        public static Texture2D guiButton;

        public static GUISkin defaultSkin;
        public static GUISkin nodeSkin;
        public static GUIStyle nodeLabel;
        public static GUIStyle nodeLabelBold;
        public static GUIStyle nodeLabelSelected;
        public static GUIStyle nodeBox;
        public static GUIStyle nodeBoxBold;

        public static List<Node> nodes = new List<Node>();
        public static bool Init()
        {
            background = ResourceManager.LoadTexture("Textures/background.png");
            guiBox = ResourceManager.LoadTexture("Textures/NE_Box.png");

            nodeSkin = Object.Instantiate<GUISkin>(GUI.skin);

            nodeSkin.label.normal.textColor = textColor;
            nodeLabel = nodeSkin.label;

            nodeSkin.box.normal.textColor = textColor;
            nodeSkin.box.normal.background = guiBox;
            nodeBox = nodeSkin.box;

            nodeSkin.button.normal.textColor = textColor;
            
            nodeLabelBold = new GUIStyle(nodeLabel);
            nodeLabelBold.fontStyle = FontStyle.Bold;

            nodeLabelSelected = new GUIStyle(nodeLabel);
            
            nodeBoxBold = new GUIStyle(nodeBox);
            nodeBoxBold.fontStyle = FontStyle.Bold;
            return true;
        }


        public static void InputEvent()
        {
            Event e = Event.current;
            Vector2 mousePos = e.mousePosition;

            bool leftClick = e.button == 0;
            bool rightClick = e.button == 1;
            bool mouseDown = e.type == EventType.MouseDown;
            bool mouseUp = e.type == EventType.MouseUp;

            if (rightClick && mouseDown)
            {
                Debug.Log("right click");
                Node node = Node.Create("node_id", mousePos);
                nodes.Add(node);
                /*
                NodeEditorFramework.Utilities.GenericMenu menu = new NodeEditorFramework.Utilities.GenericMenu();
                menu.AddItem(new GUIContent("create node"), false, () => {
                    Debug.Log("create node");
                });
                menu.ShowAsContext();
                e.Use();
                */
            }
        }
    }

}