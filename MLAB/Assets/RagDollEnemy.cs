using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollEnemy : MonoBehaviour
{
    private Animator animator;
    
    private Rigidbody[] rigidBodies;
    private Collider[] colliders;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
      
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.isKinematic = false;
            rb.AddForce((Vector3.up + Vector3.forward) * 10f, ForceMode.Impulse);
        }
        
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

      
    }


}
