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

    WanderMovement wanderMovement;
    PatrolMovement patrolMovement;
    EVAShooting evaShooting;
    EVAInfo evaInfo;

    public override TaskStatus OnUpdate()
    {
        wanderMovement = targetGameobject.GetComponent<WanderMovement>();
        patrolMovement = targetGameobject.GetComponent<PatrolMovement>();
        evaShooting = targetGameobject.GetComponent<EVAShooting>();
        evaInfo = targetGameobject.GetComponent<EVAInfo>();

        targetAnimator.SetBool("isRunning", true);

        if (evaInfo.isWandering == false)
        {
            patrolMovement._navMeshAgent.SetDestination(evaShooting._spawnPoint.position);
            Debug.Log("Going to spawn patrol (spawn): " + evaShooting._spawnPoint.position.ToString());
            Debug.Log("Going to spawn patrol (nav): " + patrolMovement._navMeshAgent.destination.x + patrolMovement._navMeshAgent.destination.y + patrolMovement._navMeshAgent.destination.z);
            if (!patrolMovement._navMeshAgent.pathPending && patrolMovement._navMeshAgent.remainingDistance < 0.5f)
            {
                evaShooting.RechargeBullets();
            }
        }
        else { 
            wanderMovement.PutNewWanderPoint(evaShooting._spawnPoint);
            Debug.Log("Going to spawn wander: " + evaShooting._spawnPoint.position.ToString());
            Debug.Log("Going to spawn patrol (wander): " + wanderMovement._navMeshAgent.destination.x + wanderMovement._navMeshAgent.destination.y + wanderMovement._navMeshAgent.destination.z);

            if (!wanderMovement._navMeshAgent.pathPending && wanderMovement._navMeshAgent.remainingDistance < 0.5f)
            {
                evaShooting.RechargeBullets();
            }
        }

        return TaskStatus.COMPLETED;
    }
}