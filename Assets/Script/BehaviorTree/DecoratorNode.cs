namespace BehaviorTree
{
    //装饰节点，用于装饰子节点所执行的功能,这类节点只能拥有一个子节点
    public abstract class DecoratorNode : BaseNode
    {
        public DecoratorNode()
        {
            maxChild = NodeChildLimit.ONLY;
        }

        public override int NodeID { get; set; }

        public override INode Enter()
        {
            return this;
        }

        public abstract override ProcessResult Process();

        public INode child;
    }

    //对执行结果取反
    public class OppositeNode : DecoratorNode
    {
        public override ProcessResult Process()
        {
            ProcessResult result = child.Process();
            if (result == ProcessResult.Success)
                return ProcessResult.Failed;
            else if (result == ProcessResult.Failed)
                return ProcessResult.Success;

            return result;
        }
    }
}