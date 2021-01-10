using UnityEngine;
using BehaviorTree;
public class Jump
{
    public ProcessResult JumpFunc(TreeContext context)
    {
        GameObject owner = context.owner;
        UnityChanController controller = owner.GetComponent<UnityChanController>();
        controller.Jump(JumpCallback);
        Debug.Log("Jump");
        return ProcessResult.Running;
    }

    public void JumpCallback(bool succ, TreeContext context)
    {
        Debug.Log("Procress Jump");
        //context.Resume(0);
    }
}
