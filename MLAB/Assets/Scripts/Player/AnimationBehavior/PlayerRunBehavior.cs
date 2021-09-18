using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehavior : StateMachineBehaviour
{
   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttackSystem.runOnce = false;
        CameraManager.instance.ToggleFollowCam(true);

        if (animator.GetComponent<PlayerAttackSystem>().GetCurrentPower() != PowerType.Timer)
        {
            animator.GetComponent<PlayerMovement>().SetMoveSpeed(10f);
        }
    }
}
