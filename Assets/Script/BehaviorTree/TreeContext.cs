using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class TreeContext
    {
        public TreeContext(INode root)
        {
            this.root = root;
            this.isRuning = true;
        }
        public void Update()
        {
            if (subtree.Count != 0)
            {
                subtree.Clear();
            }

            DoUpdate(this.root);
        }

        public void Resume(int id)
        {
            if (subtree.Count == 0 || subtree.Peek().NodeID != id)
            {
                return;
            }

            INode node = subtree.Pop();
            DoUpdate(node.Leave());
        }

        private void DoUpdate(INode node)
        {
            while (node != null)
            {
                node = node.Enter();
                if (node != null)
                {
                    ProcessResult result = node.Process();
                    if (result == ProcessResult.Running)
                    {
                        break;
                    }
                    node = node.Leave();
                }

            }
        }

        public void EnterSubtree(INode node)
        {
            subtree.Push(node);
        }

        public bool isRuning;
        public GameObject owner;

        Stack<INode> subtree = new Stack<INode>();
        INode root;
    }
}