using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/State Machine/States/Idle" , order = 1)]
public class IdleState : AbstractFSMState
{
    [SerializeField]
    float idleDuration = 3f;

    float totalDuration;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        enteredState = base.EnterState();
        if (enteredState)
        {
            //Debug.Log("ENTERED IDLE STATE.");
            totalDuration = 0f;
        }
        return enteredState;
    }

    public override void UpdateState()
    {
        if (enteredState)
        {
            totalDuration += Time.deltaTime;
            //Debug.Log("UPDATING IDLE STATE. " + totalDuration + "Seconds. ");
            
            if(totalDuration >= idleDuration)
            {
                fsm.EnterState(FSMStateType.PATROL);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        //Debug.Log("EXITTING IDLE STATE.");
        return true;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
