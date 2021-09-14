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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
   
}

