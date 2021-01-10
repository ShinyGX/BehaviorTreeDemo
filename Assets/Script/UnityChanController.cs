using System;
using System.Collections;
using UnityChan;
using UnityEngine;
using BehaviorTree;

public class UnityChanController : UnityChanControlScriptWithRgidBody
{
    public float vertical, horizontal;
    public bool jump;
    Coroutine currentMoveCoroutine, currentJumpCoroutine;

    public override void Start()
    {
        base.Start();
        vertical = 0;
        horizontal = 0;
        jump = false;
    }

    private TreeContext GetTreeContext()
    {
        return this.gameObject.GetComponent<AiComp>().behavior;
    }

    public override bool GetJump()
    {
        return jump;
    }

    public override float GetHorizontal()
    {
        return horizontal;
    }

    public override float GetVertical()
    {
        return vertical;
    }

    public void Jump(Action<bool, TreeContext> callback)
    {
        if (currentJumpCoroutine != null)
            StopCoroutine(currentJumpCoroutine);
        currentJumpCoroutine = StartCoroutine(DoJump(callback));
    }

    private IEnumerator DoJump(Action<bool, TreeContext> callback)
    {
        jump = true;
        this.anim.SetBool("Jump", true);
        yield return new WaitForFixedUpdate();
        jump = false;
        while (this.anim.GetBool("Jump"))
        {
            Debug.Log("Jump" + this.anim.GetBool("Jump"));
            yield return new WaitForFixedUpdate();
        }
        callback(true, GetTreeContext());
    }

    public void MoveToTarget(Vector3 position, Action<bool, TreeContext> callback)
    {
        if (currentMoveCoroutine != null)
        {
            StopCoroutine(currentMoveCoroutine);
            currentMoveCoroutine = null;
        }
        currentMoveCoroutine = StartCoroutine(DoMoveToTarget(position, callback));
    }

    private IEnumerator DoMoveToTarget(Vector3 target, Action<bool, TreeContext> callback)
    {
        Vector3 dir = target - this.gameObject.transform.position;
        dir.y = 0;
        dir = Vector3.ClampMagnitude(dir, 1);
        while(Mathf.Abs(Vector3.Distance(target, this.gameObject.transform.position)) > 1f)
        {
            vertical = dir.z;
            horizontal = dir.x;
            //Debug.Log(Vector3.Distance(target, this.gameObject.transform.position));
            yield return new WaitForEndOfFrame();
        }

        vertical = 0;
        horizontal = 0;
        callback(true, GetTreeContext());
        currentMoveCoroutine = null;
    }
}
