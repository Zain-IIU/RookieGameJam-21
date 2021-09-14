using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantTrigger : MonoBehaviour
{
    [SerializeField]
    int ID;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") )
        { 
            EventManager.instance.PlayerEntered(ID);
        }
    }
}
