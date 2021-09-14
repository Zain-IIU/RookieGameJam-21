using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehavior : StateMachineBehaviour
{
    public bool enableActionCam2;
    public bool performNormalAttack;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (performNormalAttack || GameManager.instance.isGameOver) return;
        
        animator.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
        CameraManager.instance.ToggleFollowCam(false);
        /*
        if (performNoAction)
        {
            animator.GetComponent<PlayerMovement>().SetMoveSpeed(7f); 
            return;
        }
        */
        
        PlayerMovement.isPerformingAttack = true;
        if (!enableActionCam2)
        {
            CameraManager.instance.ToggleActionCam_01(true);
        }
        else
        {
            CameraManager.instance.ToggleActionCam_02(true);
        }
      
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        PlayerAttackSystem.instance.runOnce = false;

        if (performNormalAttack) return;
        PlayerMovement.isPerformingAttack = false;
        PlayerAnimationsHandler.instance.ResetPlayerPowers(PowerType.Null);

       
        CameraManager.instance.ToggleFollowCam(true);
        CameraManager.instance.ToggleActionCam_01(false);
        CameraManager.instance.ToggleActionCam_02(false);

        PlayerAttackSystem.instance.EnableFootTrailEffects(false, false);
    }


}
