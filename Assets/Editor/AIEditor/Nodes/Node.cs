﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
namespace AIEditor
{
    public abstract class Node : ScriptableObject
    {
        public Rect rect;
        public Node parent;
        public string title;
        public List<Node> children = new List<Node>();
        // Use this for initialization

        public abstract void DrawNode();

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

    public static class NodeCreator
    {
        public delegate Node CreateNodeDelegate();
        public static Dictionary<string, CreateNodeDelegate> creator;

        public static void Init()
        {
            creator = new Dictionary<string, CreateNodeDelegate>();

            Register("Selector", () => { 
                Selector node = new Selector();
                node.title = "Selector";
                node.rect = new Rect(0, 0, 150, 100);
                return node;
            });

            Register("Sequence", () =>
            {
                Sequence node = new Sequence();
                node.title = "Sequence";
                node.rect = new Rect(0, 0, 150, 100);
                return node;
            });
        }

        public static void Register(string nodeType, CreateNodeDelegate dele)
        {
            if(creator.ContainsKey(nodeType))
            {
                throw new System.Exception(nodeType + " is already registered");
            }

            creator.Add(nodeType, dele);
        }

        public static Node CreateInstance(string nodeType)
        {
            if (false == creator.ContainsKey(nodeType))
            {
                throw new System.Exception(nodeType + " is not node type");
            }

            return creator[nodeType]();
        }
    }
}