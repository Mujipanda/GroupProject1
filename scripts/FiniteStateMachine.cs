using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FiniteStateMachine : MonoBehaviour
{
    AbstractFSMState currentState;

    [SerializeField]
    List<AbstractFSMState> validStates;
    Dictionary<FSMStateType, AbstractFSMState> fsmStates;

    public void Awake()
    {
        currentState = null;

        fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();
        NavMeshAgent navmeshAgent = GetComponent<NavMeshAgent>();
        NPC npc = GetComponent<NPC>();

        foreach(AbstractFSMState state in validStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNPC(npc);
            state.SetNavMeshAgent(navmeshAgent);
            fsmStates.Add(state.StateType, state);

        }
    }

    public void Start()
    {
        EnterState(FSMStateType.IDLE);
    }

    public void Update()
    {
        if (currentState != null)
            currentState.UpdateState();
    }

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
            return;
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = nextState;
        currentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        if (fsmStates.ContainsKey(stateType))
        {
            AbstractFSMState nextState = fsmStates[stateType];
            EnterState(nextState);
        }
    }
}
