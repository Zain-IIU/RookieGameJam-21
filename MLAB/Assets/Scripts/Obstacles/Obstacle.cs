using System;
using System.Collections;
using UnityEngine;

using DG.Tweening;
public class Obstacle : MonoBehaviour
{

    [SerializeField]
    bool isMoveable;
    [SerializeField]
    PowerType power;
    private float time = 0;
    public float amplitude = 2;
    public float occilation = 0.5f;


    public static bool fallOnce;
   
    private void Update()
    {
        time += Time.deltaTime;
        
        if (isMoveable)
        {
            Vector3 moveX = transform.localPosition;
            float x = amplitude * Mathf.Sin(time * occilation);
            moveX.x = x;

            transform.localPosition = moveX;
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        bool playerTag = other.gameObject.CompareTag("Player");

        Debug.Log(fallOnce);

        if(playerTag)
        {
            if(!fallOnce)
            {
                Debug.Log("Game not Over");
                fallOnce = true;
                other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
                other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
                PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
            }
            //todo game over logic
            else
            {
                Debug.Log("Game Over");
                other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
                other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
                PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
                GameManager.instance.isGameOver = true;
                UIManager.instance.OnGameOver();
                fallOnce = false;
            }
        }
        

    }


}
