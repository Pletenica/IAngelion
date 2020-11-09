using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EVAMovement : MonoBehaviour
{
    public bool EVAControl = true;
    public NavMeshAgent _navMeshAgent;

    //Patrol variables
    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        PutWellPatrolPoints();
    }

    public void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length > 0)
        {
            _navMeshAgent.destination = patrolPoints[currentPatrolPoint].position;

            currentPatrolPoint++;
            currentPatrolPoint %= patrolPoints.Length;
        }
    }

    public void PutWellPatrolPoints()
    {
        patrolPoints[0] = GameObject.Find("Patrol 1").transform;
        patrolPoints[1] = GameObject.Find("Patrol 2").transform;
        patrolPoints[2] = GameObject.Find("Patrol 3").transform;
        patrolPoints[3] = GameObject.Find("Patrol 4").transform;
        patrolPoints[4] = GameObject.Find("Patrol 5").transform;
        patrolPoints[5] = GameObject.Find("Patrol 6").transform;
    }


    public void NextWanderPoint()
    {
        Vector3 randomDirection = getNewRandomPosition();

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 30, 1);
        Vector3 finalPosition = hit.position;

        GetComponent<NavMeshAgent>().destination = finalPosition;
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-45, 45);
        float z = Random.Range(-45, 45);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }
}
