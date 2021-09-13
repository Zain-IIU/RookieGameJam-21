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

    private bool fallOnce;
    private void OnTriggerEnter(Collider other)
    {
        bool playerTag = other.gameObject.CompareTag("Player");

        if (!fallOnce && playerTag)
        {
            fallOnce = true;
            other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
            other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
            PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
        }
        else
        {
            // todo put game over logic
        }

    }


}
