using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetTrigger("CowardEnemy");
        ResetColliders(false);
        ResetRigidBody(true);
        
    }


    public void ResetRigidBody(bool state)
    {
        
        Rigidbody[] rb = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rb)
        {
            rigidbody.isKinematic = state;
        }
        
    }


    public void ResetColliders(bool state)
    {
        Collider[] col = GetComponentsInChildren<Collider>();

        foreach (Collider collider in col)
        {
         
            collider.enabled = state;
        }
        GetComponent<Collider>().enabled = !state;
    }
}
