using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is EVA patrolling?")]
[Help("Checks if EVA is patrolling.")]
public class IsEVAPatrolling : ConditionBase
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    public override bool Check()
    {
        EVAInfo evaInfo = targetGameobject.GetComponent<EVAInfo>();

        return evaInfo.isWandering==false;
    }
}