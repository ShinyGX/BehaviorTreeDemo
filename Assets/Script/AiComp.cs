using System.Collections;
using UnityEngine;
using BehaviorTree;

public class AiComp : MonoBehaviour
{
    public TreeContext behavior;
    // Start is called before the first frame update
    void Start()
    {
        var tree = new UnityChanBehaviorTreeBuilder();
        behavior = tree.context;
        behavior.owner = this.gameObject;

        StartCoroutine(BehaviorUpdate());
    }

    IEnumerator BehaviorUpdate()
    {
        while (true)
        {
            if (behavior != null && behavior.isRuning)
            {
                Debug.Log("Behavior Updata");
                behavior.Update();
            }

            yield return new WaitForSeconds(15);
        }
    }

}
