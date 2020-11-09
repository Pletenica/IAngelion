using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is EVA just arribed to a patrol point?")]
[Help("Checks if EVA is going to a patrol point.")]
public class IsEVAinPath : ConditionBase
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    public override bool Check()
    {
        bool ret = false;
        EVAMovement evaMovement = targetGameobject.GetComponent<EVAMovement>();

        if (!evaMovement._navMeshAgent.pathPending && evaMovement._navMeshAgent.remainingDistance < 0.5f)
        {
            ret = true;
        }
        
        return ret;
    }
}