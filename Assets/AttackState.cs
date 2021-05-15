using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    AgentStateMachine agentStateMachine;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentStateMachine = animator.GetComponent<AgentStateMachine>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(agentStateMachine.Target.position, agentStateMachine.transform.position) <= agentStateMachine.AttackRange)
        {
            agentStateMachine.AttackState(true);
        }
        else
        {
            agentStateMachine.SetupAgent("Goal");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agentStateMachine.AttackState(false);
    }
}