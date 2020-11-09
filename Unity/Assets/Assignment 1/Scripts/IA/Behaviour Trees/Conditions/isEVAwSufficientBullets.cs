using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is EVA without sufficients bullets?")]
[Help("Checks if EVA is with sufficients bullets to fire.")]
public class IsEVAwSufficientBullets : ConditionBase
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    public override bool Check()
    { 
        EVAShooting evaShooting = targetGameobject.GetComponent<EVAShooting>();

        return evaShooting.countBullets==0;
    }
}
