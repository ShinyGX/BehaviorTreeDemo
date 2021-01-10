using UnityEngine;
using BehaviorTree;

public class CheckDistance
{


    public ProcessResult CheckDistanceTo(TreeContext context)
    {
        GameObject owner = context.owner;
        if(Vector3.Distance(owner.transform.position, new Vector3(0, 0, 10)) < 1f)
            return ProcessResult.Success;
        return ProcessResult.Failed;
    }

    public ProcessResult CheckDistanceMore(TreeContext context)
    {
        GameObject owner = context.owner;
        if (Vector3.Distance(owner.transform.position, new Vector3(0, 0, 10)) > 1f)
        {
            return ProcessResult.Success;
        }
        return ProcessResult.Failed;
    }
}
