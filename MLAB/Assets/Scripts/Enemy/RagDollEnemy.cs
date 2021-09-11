using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollEnemy : MonoBehaviour
{
    private Animator animator;
    
    private Rigidbody[] rigidBodies;
    private Collider[] colliders;

    private Transform playerTarget;
    [SerializeField]
    bool isBoss;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if(!isBoss)
        animator.SetTrigger("CowardEnemy");
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = true;
        }
        
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        animator.enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }


    public void EnableRagdoll()
    {
        animator.enabled = false;
        Vector3 direction = playerTarget.position - transform.position;
        direction.Normalize();
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f ,0f) + (-direction) * 15f + Vector3.up * 10f, ForceMode.Impulse);
        }
        
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;

    }


}
