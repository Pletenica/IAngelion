using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Set Run Animation")]
[Help("Shot Bullets")]
public class SetRunAnim : BasePrimitiveAction
{
    [InParam("animator")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public Animator targetAnimator;

    public override TaskStatus OnUpdate()
    {
        targetAnimator.SetBool("isRunning", true);

        return TaskStatus.COMPLETED;
    }
}
