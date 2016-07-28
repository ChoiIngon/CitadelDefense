using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
using System.Linq;
#endif

namespace AI
{
    public class BehaviourTree : MonoBehaviour
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

            public void AddChild(Node child)
            {
                children.Add(child);
            }

            public Node GetChild(int index = 0)
            {
                return children[index];
            }

            public abstract bool Update();

            public virtual void DrawNode() { }
            public void DrawConnection()
            {
                foreach (Node child in children)
                {
                    DrawConnection(rect, child.rect);
                }
            }
            public static void DrawConnection(Rect start, Rect end)
            {
#if UNITY_EDITOR
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
#endif
            }
        }
        
        public class RandomSelector : Node
        {
            public override bool Update()
            {
                List<Node> copied = new List<Node>(children);
                for (int i = 0; i < copied.Count; i++)
                {
                    Node temp = copied[i];
                    int randomIndex = Random.Range(i, copied.Count);
                    copied[i] = copied[randomIndex];
                    copied[randomIndex] = temp;
                }

                foreach (Node child in copied)
                {
                    if (true == child.Update())
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        
        public class Succeeder : Node
        {  // A succeeder will always return success, irrespective of what the child node actually returned. These are useful in cases where you want to process a branch of a tree where a failure is expected or anticipated, but you don’t want to abandon processing of a sequence that branch sits on.
            public override bool Update()
            {
                GetChild().Update();
                return true;
            }
        }
        public class Failer : Node
        {  // The opposite of a Succeeder, always returning fail.  Note that this can be achieved also by using an Inverter and setting its child to a Succeeder.
            public override bool Update()
            {
                GetChild().Update();
                return false;
            }
        }
        public class Repeater : Node
        {  // A repeater will reprocess its child node each time its child returns a result. These are often used at the very base of the tree, to make the tree to run continuously. Repeaters may optionally run their children a set number of times before returning to their parent.
            const int NOT_FOUND = -1;
            int numRepeats;

            public Repeater(int num = NOT_FOUND)
            {
                numRepeats = num;
            }

            public override bool Update()
            {
                for (int i = 0; i < numRepeats - 1; i++)
                {
                    GetChild().Update();
                }
                return GetChild().Update();
            }
        };
        public class RepeatUntilFail : Node
        {  // Like a repeater, these decorators will continue to reprocess their child. That is until the child finally returns a failure, at which point the repeater will return success to its parent.
            public override bool Update()
            {
                while (GetChild().Update()) { }
                return true;
            }
        };

        public Node root;
        public ScriptableObject assetData;
        // Use this for initialization
        void Start()
        {
            NodeManager manager = (NodeManager)Object.Instantiate<ScriptableObject>(assetData);
            Debug.Log(manager.nodes.Count);
            root = manager.root;
        }

        // Update is called once per frame
        void Update()
        {
            if (null == root)
            {
                return;
            }
            root.Update();
        }
    }

    public class NodeAttribute : System.Attribute
    {
        public string contextText { get; private set; }
        public NodeAttribute(string ReplacedContextText)
        {
            contextText = ReplacedContextText;
        }
    }
}
