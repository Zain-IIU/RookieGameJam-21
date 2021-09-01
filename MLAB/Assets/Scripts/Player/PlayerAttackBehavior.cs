using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBehavior : StateMachineBehaviour
{
    public bool enableActionCam2;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
        PlayerMovement.isPerformingAttack = false;
        PlayerAttacking.runOnce = false;
        
        CameraManager.instance.ToggleFollowCam(true);
        CameraManager.instance.ToggleActionCam_01(false);
        CameraManager.instance.ToggleActionCam_02(false);
       
    }


}
