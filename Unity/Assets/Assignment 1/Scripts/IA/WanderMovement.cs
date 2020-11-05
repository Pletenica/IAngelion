using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderMovement : MonoBehaviour
{
    public bool EVAControl = true;

    public Animator EVAAnimator;
    public NavMeshAgent _navMeshAgent;
    [Range(3, 25)]
    public float maxWanderTimer = 7.0f;
    private float _actualTimer;
    [Range(7, 30)]
    public float wanderRange = 15;

    // Update is called once per frame
    void Update()
    {
        //Timer functionallity
        _actualTimer += Time.deltaTime;

        //If timer is greater than maxtimer put another destination and reset timer.
        if (_actualTimer >= maxWanderTimer)
        {
            Vector3 newPos = Wander(transform.position, wanderRange);
            _navMeshAgent.SetDestination(newPos);
            _actualTimer = 0;
        }

        //Put Animation Well
        if (_navMeshAgent.hasPath == false)
        {
            EVAAnimator.SetBool("isRunning", false);
        }
        else if (_navMeshAgent.hasPath == true)
        {
            EVAAnimator.SetBool("isRunning", true);
        }
    }

    public Vector3 Wander(Vector3 _actualPosition, float _wanderRange)
    {
        Vector3 randDirection = Random.insideUnitSphere * _wanderRange;
        randDirection += _actualPosition;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, _wanderRange, NavMesh.AllAreas);

        return navHit.position;
    }
}
