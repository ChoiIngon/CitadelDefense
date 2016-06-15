using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AIEditor
{
    public abstract class Node : ScriptableObject
    {
        [HideInInspector]
		public int id;
        [HideInInspector]
        public Node parent;
		[SerializeField]
        public List<Node> children = new List<Node>();

        public string title;
		[SerializeField]
        public Rect rect;

        /*
        public enum NodeType
        {
            Condition,
            Action,
            Decorator,
            Max
        }
        protected NodeType type;
        static Color[] NODE_COLOR = new Color[(int)NodeType.Max];
        */
        public abstract void DrawNode();
        public abstract string ToString();

        public void DrawConnection()
        {
            foreach(Node child in children)
            {
                DrawConnection(rect, child.rect);
            }
        }

        public static void DrawConnection(Rect start, Rect end)
        {
            Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height, 0);
            Vector3 endPos = new Vector3(end.x + end.width / 2, end.y, 0);
            Vector3 startTan = startPos + Vector3.up * 50;
            Vector3 endTan = endPos + Vector3.down * 50;
            Color shadowCol = new Color(0, 0, 0, .06f);

            for (int i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
            }

            Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
        }
    }

    
	public class NodeAttribute : Attribute 
	{
		public string contextText { get; private set; }

		public NodeAttribute (string ReplacedContextText) 
		{
			contextText = ReplacedContextText;
		}
	}
}