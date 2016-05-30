using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace BehaviourTree
{
    public class Node : ScriptableObject
    {
        public Rect rect = new Rect();
        public List<Node> children = new List<Node>();

        public virtual void DrawNode()
        {
            const float TITLE_HEIGHT = 20.0f;
            Rect nodeRect = rect;

            Rect headerRect = new Rect(nodeRect.x, nodeRect.y, nodeRect.width, nodeRect.y + TITLE_HEIGHT);
            GUI.Label(headerRect, name);

            Rect bodyRect = new Rect(nodeRect.x, nodeRect.y + 20, nodeRect.width, nodeRect.height - TITLE_HEIGHT);
            GUI.BeginGroup(bodyRect);
            /*
            bodyRect.position = Vector2.zero;
            GUILayout.BeginArea(bodyRect);
            GUI.changed = false;
            NodeGUI();
            GUILayout.EndArea ();
			GUI.EndGroup ();
            */
        }

        public virtual void DrawConnection()
        {
            if(EventType.Repaint != Event.current.type)
            {
                return;
            }
            Vector2 startPos = new Vector2(rect.width / 2, rect.height);
            Vector2 startTan = startPos + Vector2.down * 50;

            Color shadowCol = new Color(0, 0, 0, 0.06f);
            foreach(Node child in children)
            {
                Vector2 endPos = new Vector2(child.rect.width / 2, rect.y);
                Vector2 endTan = endPos + Vector2.up * 50;
                Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, 1);
            }
        }
        protected virtual void NodeGUI () {}

        public static Node Create(string nodeID, Vector2 position)
        {
            Node node = new Node();
            
            return node;
        }
    }

    
}