using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Recharge Bullets")]
[Help("Go to recharge Bullets")]
public class HideBB : BasePrimitiveAction
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    [InParam("animator")]
    [Help("Game object animator")]
    public Animator targetAnimator;

    EVAMovement evaMovement;
    EVAShooting evaShooting;
    EVAInfo evaInfo;

    public override TaskStatus OnUpdate()
    {
        evaMovement = targetGameobject.GetComponent<EVAMovement>();
        evaShooting = targetGameobject.GetComponent<EVAShooting>();
        evaInfo = targetGameobject.GetComponent<EVAInfo>();

        targetAnimator.SetBool("isRunning", true);

        evaMovement._navMeshAgent.SetDestination(evaShooting._spawnPoint.position);
        if (!evaMovement._navMeshAgent.pathPending && evaMovement._navMeshAgent.remainingDistance < 0.5f)
        {
            evaShooting.RechargeBullets();
        }

        return TaskStatus.COMPLETED;
    }
}