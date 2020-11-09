using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Shot Bullet")]
[Help("Shot Bullets")]
public class ShotBullet : BasePrimitiveAction
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    public override TaskStatus OnUpdate()
    {
        EVAShooting evaShooting = targetGameobject.GetComponent<EVAShooting>();

        evaShooting.Fire();

        return TaskStatus.COMPLETED;
    }
}