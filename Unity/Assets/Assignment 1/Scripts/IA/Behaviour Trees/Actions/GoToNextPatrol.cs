﻿using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/Go To Next Patrol")]
[Help("Go To Next Patrol")]
public class GoToNextPatrol : BasePrimitiveAction
{
    [InParam("game object")]
    [Help("Game object to add the component, if no assigned the component is added to the game object of this behavior")]
    public GameObject targetGameobject;

    public override TaskStatus OnUpdate()
    {
        EVAMovement evaMovement = targetGameobject.GetComponent<EVAMovement>();

        evaMovement.PutWellPatrolPoints();
        evaMovement.MoveToNextPatrolPoint();

        return TaskStatus.COMPLETED;
    }
}