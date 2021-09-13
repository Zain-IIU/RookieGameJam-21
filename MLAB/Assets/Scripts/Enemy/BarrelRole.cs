using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BarrelRole : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float anglerperRotation;
    [SerializeField] private float timetoDestroy;
    private float timer;
    [SerializeField] GameObject explosionVFX;

    private void Start()
    {
        timer = timetoDestroy;
    }

    private void Update()
    {
        transform.Rotate(1*rotationSpeed, 0, 0);
        transform.Translate(-Vector3.forward * (movingSpeed * Time.deltaTime),Space.World);

        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!PlayerAttackSystem.runOnce && collision.gameObject.CompareTag("Player"))
        {
            PlayerAttackSystem.runOnce = true;
            collision.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
            collision.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
            PlayerAnimationsHandler.instance.ResetPlayerPowers(PowerType.Null);
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

