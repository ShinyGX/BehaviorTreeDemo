using BehaviorTree;
using UnityEngine;

public class MoveTo
{
    public ProcessResult MoveToTarget(TreeContext context)
    {
        GameObject owner = context.owner;
        MoveComp controller = owner.GetComponent<MoveComp>();
        controller.MoveToAsync(new Vector3(0, 0, 10), MoveToTargetCallback);
        Debug.Log("do move");
        return ProcessResult.Running;
    }

    public void MoveToTargetCallback(bool succ, TreeContext context)
    {
        context.Resume(1);
    }
}