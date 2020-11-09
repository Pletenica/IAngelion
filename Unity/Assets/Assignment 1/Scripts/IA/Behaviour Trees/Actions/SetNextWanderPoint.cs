using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Next Wander Point")]
[Help("Go to recharge Bullets")]
public class SetNextWanderPoint : BasePrimitiveAction
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    EVAMovement evaMovement;

    public override TaskStatus OnUpdate()
    {
        evaMovement = targetGameobject.GetComponent<EVAMovement>();

        evaMovement.NextWanderPoint();

        return TaskStatus.COMPLETED;
    }
}