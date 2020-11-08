using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolMovement : MonoBehaviour
{
    public bool EVAControl = true;
    public Animator EVAAnimator;
    public NavMeshAgent _navMeshAgent;
    bool gotoSpawn=false;
    public Transform[] patrolPoints;
    public Transform _spawnPoint;
    public WaitForSeconds rechargeseconds;

    private int currentPatrolPoint = 0;
    private EVAShooting evaShooting;

    // Start is called before the first frame update
    void Start()
    {
        evaShooting = GetComponent<EVAShooting>();
        PutWellPatrolPoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (_navMeshAgent.enabled == true && EVAControl == true)
        {
            _navMeshAgent.isStopped = false;
            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance < 0.5f)
            {
                if (gotoSpawn == false)
                {
                    MoveToNextPatrolPoint();
                    if(evaShooting.countBullets==0) evaShooting.RechargeBullets();
                }
                else
                {
                    _navMeshAgent.destination = _spawnPoint.position;
                    gotoSpawn = false;
                }
            }
            EVAAnimator.SetBool("isRunning", true);
        }
        if (EVAControl == false)
        {
            EVAAnimator.SetBool("isRunning", false);
            _navMeshAgent.isStopped=true;
        }
    }

    public void MoveToNextPatrolPoint()
    {
        if(patrolPoints.Length > 0)
        {
            _navMeshAgent.destination = patrolPoints[currentPatrolPoint].position;

            currentPatrolPoint++;
            currentPatrolPoint %= patrolPoints.Length;
        }
    }

    void PutWellPatrolPoints()
    {
        patrolPoints[0] = GameObject.Find("Patrol 1").transform;
        patrolPoints[1] = GameObject.Find("Patrol 2").transform;
        patrolPoints[2] = GameObject.Find("Patrol 3").transform;
        patrolPoints[3] = GameObject.Find("Patrol 4").transform;
        patrolPoints[4] = GameObject.Find("Patrol 5").transform;
        patrolPoints[5] = GameObject.Find("Patrol 6").transform;
    }

    public void PutSpawnPointDestination()
    {
        gotoSpawn = true;
        currentPatrolPoint = 0;
    }
}
