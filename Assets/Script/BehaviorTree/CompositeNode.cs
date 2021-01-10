using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    //调度节点
    public abstract class CompositeNode : BaseNode
    {
        public override int NodeID { get; set; }
        public override INode Enter()
        {
            return this;
        }

        public abstract override ProcessResult Process();

        // 进入自己的子节点
        private INode EnterSubtree()
        {
            if (currentNode < nodes.Count)
            {
                INode node = nodes[currentNode];
                currentNode++;
                return node;
            }

            return null;
        }

        int currentNode = 0;

        // 自身子树所拥有的节点
        public List<INode> nodes = new List<INode>();
    }

    //顺序调度，直到子节点调用失败为止
    public class SequenceNode : CompositeNode
    {

        public override ProcessResult Process()
        {
            foreach (INode node in nodes)
            {
                ProcessResult result = node.Process();
                if (result == ProcessResult.Failed)
                    return ProcessResult.Failed;
                if (result == ProcessResult.Running)
                    return result;
            }

            return ProcessResult.Success;
        }
    }

    //顺序调度，直到子节点调用成功为止
    public class SelectorNode : CompositeNode
    {
        public override ProcessResult Process()
        {
            foreach(INode node in nodes)
            {
                ProcessResult result = node.Process();
                if (result == ProcessResult.Success)
                    return ProcessResult.Success;
                if (result == ProcessResult.Running)
                    return result;
            }
            return ProcessResult.Failed;
        }
    }

    //无论如何都会跑完
    public class TickNode : CompositeNode
    {
        public override ProcessResult Process()
        {
            foreach(INode node in nodes)
            {
                ProcessResult result = node.Process();
                if (result == ProcessResult.Running)
                    return result;
            }

            return ProcessResult.Success;
        }
    }
}