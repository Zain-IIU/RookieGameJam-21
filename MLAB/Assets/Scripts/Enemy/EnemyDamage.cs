using System;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float enemyDamageForce = 20f;
    [SerializeField] private float forceAmount = 30f;

    [SerializeField] private GameObject enemyDeadFX;

    private bool isDestroyed;
    private bool isTrigger;

    private Rigidbody rb;
    private RagDollEnemy ragDollEnemy;

    private void Awake()
    {
        if (GetComponent<RagDollEnemy>() != null)
        {

            ragDollEnemy = GetComponent<RagDollEnemy>();
            isTrigger = true;
        }
        else
            GetComponent<Collider>().isTrigger = true;

        rb = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;
        if (collision.gameObject.CompareTag("Projectile") && ragDollEnemy!=null)
        {
            Debug.Log("Projectile");
            ragDollEnemy.EnableRagdoll();
        }

        
        if (collision.gameObject.CompareTag("Player") && collision.transform.localScale != Vector3.one)
        {
            // todo add crushing sound
            Instantiate(enemyDeadFX, transform.position, enemyDeadFX.transform.rotation);
            Destroy(gameObject);
        }


        /*if(collision.gameObject.CompareTag("Projectile"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            Destroy(gameObject); 
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            if(ragDollEnemy!=null)
                 ragDollEnemy.EnableRagdoll();
            else
              rb.AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f,0f) * enemyDamageForce, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Player") && other.transform.localScale != Vector3.one)
        {
            // todo add crushing sound
            Instantiate(enemyDeadFX, transform.position, enemyDeadFX.transform.rotation);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Projectile"))
        {
            rb.AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f, 0f) * enemyDamageForce, ForceMode.Impulse);
        }

    }


}