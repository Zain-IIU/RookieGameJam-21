using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField]
    float forceAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            
        }
        Destroy(gameObject);
    }
}
