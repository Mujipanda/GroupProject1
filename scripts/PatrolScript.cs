using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/State Machine/States/Patrol", order = 2)]
public class PatrolScript : AbstractFSMState
{

    Transform[] patrolPoints;
    int patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PATROL;
        patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        enteredState = false;

        if (base.EnterState())
        {
            patrolPoints = npc.PatrolPoints;

            if (patrolPoints == null || patrolPoints.Length == 0)
            {
                Debug.LogError("PATROLSTATE: Failed To Grab Patrol Points From The NPC.");

            }
            else
            {
                if (patrolPointIndex < 0)
                    patrolPointIndex = Random.Range(0, patrolPoints.Length);
                else
                    patrolPointIndex = (patrolPointIndex + 1) %
                    patrolPoints.Length;

                SetDestination(patrolPoints[patrolPointIndex]);

                enteredState = true;
            }
        }
        return enteredState;

    }

    public override void UpdateState()
    {
        if (enteredState)
        {
            if(Vector3.Distance(navMeshAgent.transform.position, patrolPoints  [patrolPointIndex].position) <= 1f)
            {
                fsm.EnterState(FSMStateType.IDLE);
            }
        }
    }
    private void SetDestination(Transform destination)
    {
        if(navMeshAgent != null && destination != null)
        {
            navMeshAgent.SetDestination(destination.transform.position);
        }
    }
   
}
