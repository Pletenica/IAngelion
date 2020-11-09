using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Set Attack Animation")]
[Help("Shot Bullets")]
public class SetAttackAnim : BasePrimitiveAction
{
    [InParam("animator")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public Animator targetAnimator;

    public override TaskStatus OnUpdate()
    {
        targetAnimator.SetBool("isAttacking", true);

        return TaskStatus.COMPLETED;
    }
}

