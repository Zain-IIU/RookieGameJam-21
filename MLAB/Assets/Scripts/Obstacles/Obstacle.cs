using System;
using System.Collections;
using UnityEngine;

using DG.Tweening;
public class Obstacle : MonoBehaviour
{

   
    [SerializeField]
    PowerType power;
    

    public static bool fallOnce;
   
    
    
    private void OnTriggerEnter(Collider other)
    {
        bool playerTag = other.gameObject.CompareTag("Player");

      

        if(playerTag)
        {
            if (!fallOnce)
            {
               
                fallOnce = true;
                PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
                other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
                other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
                
            }
            //todo game over logic
            else
            {
                PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
                other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
                other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
                
                GameManager.instance.isGameOver = true;
                UIManager.instance.OnGameOver();
                fallOnce = false;
            }
        }
        

    }


}
