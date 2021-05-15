using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : StateMachineBehaviour
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
        if (!agentStateMachine.IsRoofObstacle)
        {
            agentStateMachine.CrouchState(false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
