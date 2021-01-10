using BehaviorTree;

public class UnityChanBehaviorTreeBuilder
{
    public TreeContext context;
    public UnityChanBehaviorTreeBuilder()
    {
        Jump jump = new Jump();
        MoveTo moveTo = new MoveTo();
        CheckDistance checkDistance = new CheckDistance();

        NodeManager.Instance.CreateActionNode(0, jump.JumpFunc);
        NodeManager.Instance.CreateActionNode(1, moveTo.MoveToTarget);

        NodeManager.Instance.CreateActionNode(2, checkDistance.CheckDistanceTo);

        NodeManager.Instance.CreateDecoratorNode(NodeType.OppositeNode, 3, 2);

        int[] childAction = { 3, 1 };
        NodeManager.Instance.CreateCompositeNode(NodeType.SequenceNode, 4, childAction);

        //NodeManager.Instance.CreateDecoratorNode(NodeType.OppositeNode, 7, 4);

        int[] childAction2 = { 4, 0 };
        NodeManager.Instance.CreateCompositeNode(NodeType.SelectorNode ,5, childAction2);

        int[] childAction3 = { 5 };
        context = NodeManager.Instance.BuildBehaviorTree(6, childAction3);

        int[] childAction4 = { 0, 1, 2, 3, 4, 5 };
        NodeManager.Instance.DeclaredTreeNode(6, childAction4, context);
    }
}
