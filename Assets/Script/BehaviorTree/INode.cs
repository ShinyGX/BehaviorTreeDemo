
namespace BehaviorTree
{
    public interface INode
    {
        INode Enter();
        ProcessResult Process();
        INode Leave();

        int NodeID { get; set; }
    }

    public abstract class BaseNode : INode
    {
        public static NodeChildLimit maxChild = NodeChildLimit.UNLIMIT;


        public abstract INode Enter();
        public abstract ProcessResult Process();

        public virtual INode Leave()
        {
            return parent;
        }


        public TreeContext context;
        //自身的树的父节点
        public INode parent;
        public abstract int NodeID { get; set; }
    }
}


