using System;

namespace BehaviorTree
{
    public delegate ProcessResult ActionFunc(TreeContext context);

    //行动节点，不允许拥有子节点，为行为树最终需要进行指向的操作，行为树唯一允许成为子节点的节点
    public class ActionNode : BaseNode
    {
        public ActionNode()
        {
            maxChild = NodeChildLimit.NONE;
        }
        public override int NodeID { get; set; }

        public override INode Enter()
        {
            return null;
        }

        public override ProcessResult Process()
        {
            if (func != null)
            {
                ProcessResult result = func(this.context);
                if (result == ProcessResult.Running)
                    this.context.EnterSubtree(this);
                return result;
            }
                

            return ProcessResult.Failed;
        }

        public ActionFunc func { set; private get; }
    }
}
