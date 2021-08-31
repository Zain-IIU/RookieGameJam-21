using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehavior : StateMachineBehaviour
{
 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CameraManager.instance.ToggleFollowCam(true);
        animator.GetComponent<PlayerMovement>().SetMoveSpeed(10f);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CameraManager.instance.ToggleFollowCam(false);
        animator.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
       
    }
}
