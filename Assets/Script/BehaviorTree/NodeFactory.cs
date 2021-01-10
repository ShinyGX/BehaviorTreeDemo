using System.Collections.Generic;

namespace BehaviorTree
{
    public enum NodeType
    {
        SequenceNode = 0,
        SelectorNode = 1,
        OppositeNode = 2,
        ActionNode = 3,
        TickNode = 4,
    }

    //一个节点能够拥有几个子节点
    public enum NodeChildLimit
    {
        ONLY, //只有一个
        NONE, //本身就是叶子节点不能再拥有子节点了
        UNLIMIT, //可以拥有无限多个
    }

    public class NodeFactory
    {
        static NodeFactory instance;
        public static NodeFactory Instance
        {
            get
            {
                if (instance == null)
                    instance = new NodeFactory();
                return instance;
            }
        }

        List<INode> nodeTypeList = new List<INode>();
        NodeFactory()
        {
            nodeTypeList.Add(new SequenceNode());
            nodeTypeList.Add(new SelectorNode());
            nodeTypeList.Add(new OppositeNode());
            nodeTypeList.Add(new ActionNode());
            nodeTypeList.Add(new TickNode());
        }
        public INode CreateNode(NodeType type, int nodeId)
        {
            INode node;
            switch (type)
            {
                case NodeType.SequenceNode:
                    node = new SequenceNode();
                    break;
                case NodeType.SelectorNode:
                    node = new SelectorNode();
                    break;
                case NodeType.OppositeNode:
                    node = new OppositeNode();
                    break;
                case NodeType.ActionNode:
                    node = new ActionNode();
                    break;
                case NodeType.TickNode:
                    node = new TickNode();
                    break;

                default:
                    return null;
            }

            node.NodeID = nodeId;
            return node;
        }
    }
}