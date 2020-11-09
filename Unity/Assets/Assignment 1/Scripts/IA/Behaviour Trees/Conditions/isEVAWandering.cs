using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is EVA wandering?")]
[Help("Checks if EVA is wandering.")]
public class IsEVAWandering : ConditionBase
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    public override bool Check()
    {
        EVAInfo evaInfo = targetGameobject.GetComponent<EVAInfo>();

        return evaInfo.isWandering == true;
    }
}