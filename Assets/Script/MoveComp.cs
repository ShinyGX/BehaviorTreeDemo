using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class MoveComp : MonoBehaviour
{
    public Vector3 targetPos;
    public UnityChanController controller;
    public Action<bool, TreeContext> callback;

    private bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<UnityChanController>();
    }
    private TreeContext GetTreeContext()
    {
        return this.gameObject.GetComponent<AiComp>().behavior;
    }

    public void MoveToAsync(Vector3 targetPos, Action<bool, TreeContext> callback)
    {
        Debug.Log("Move Async");
        isRunning = true;
        this.targetPos = targetPos;
        this.callback = callback;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
            return;

        float vertical = 0;
        float horizontal = 0;
        Vector3 dir = targetPos - this.gameObject.transform.position;
        dir.y = 0;
        dir = Vector3.ClampMagnitude(dir, 1);
        if (Vector3.Distance(targetPos, this.transform.position) > 1f)
        {
            vertical = dir.z;
            horizontal = dir.x;
        }
        else
        {

            if (callback != null)
            {
                callback(true, GetTreeContext());
            }

            isRunning = false;
        }
        Debug.Log("Update Move" + vertical + " " + horizontal);
        controller.vertical = vertical;
        controller.horizontal = horizontal;
    }
}
