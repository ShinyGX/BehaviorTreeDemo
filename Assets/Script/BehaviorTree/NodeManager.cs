
using System.Collections.Generic;

namespace BehaviorTree
{
    public class NodeManager
    {
        private static NodeManager instance;
        public static NodeManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NodeManager();
                }
                return instance;
            }
        }

        private INode CreateNode(NodeType type, int nodeId)
        {
            INode node = NodeFactory.Instance.CreateNode(type, nodeId);
            nodeMap[nodeId] = (BaseNode)node;
            return node;
        }

        //创建调度节点
        public CompositeNode CreateCompositeNode(NodeType nodeType, int nodeId, int[] childrenNodeId)
        {
            CompositeNode compositeNode = CreateNode(nodeType, nodeId) as CompositeNode;
            if (compositeNode == null)
                return null;

            foreach (int childId in childrenNodeId)
            {
                BaseNode node = nodeMap[childId];
                node.parent = compositeNode;
                compositeNode.nodes.Add(node);
            }

            return compositeNode;
        }

        //创建装饰节点
        public DecoratorNode CreateDecoratorNode(NodeType nodeType, int nodeId, int childId)
        {
            DecoratorNode decoratorNode = CreateNode(nodeType, nodeId) as DecoratorNode;
            if (decoratorNode == null)
                return null;

            BaseNode node = nodeMap[childId];
            node.parent = decoratorNode;
            decoratorNode.child = node;
            
            return decoratorNode;
        }

        //创建行动节点
        public ActionNode CreateActionNode(int nodeId, ActionFunc func)
        {
            ActionNode actionNode = CreateNode(NodeType.ActionNode, nodeId) as ActionNode;
            if (actionNode == null)
                return null;

            actionNode.func = func;
            return actionNode;
        }

        public TreeContext BuildBehaviorTree(int rootId, int[] children)
        {
            CompositeNode root = CreateCompositeNode(NodeType.TickNode, rootId, children);
            if (root == null)
                return null;

            TreeContext contxt = new TreeContext(root);
            return contxt;
        }

        public void DeclaredTreeNode(int rootId, int[] children, TreeContext context)
        {
            nodeMap[rootId].context = context;
            foreach(int child in children)
            {
                nodeMap[child].context = context;
            }
        }

        private Dictionary<int, BaseNode> nodeMap = new Dictionary<int, BaseNode>();
    }
}