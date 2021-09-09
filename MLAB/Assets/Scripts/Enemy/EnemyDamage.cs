using System;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float enemyDamangeForceAmount = 7f;
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
            rb = GetComponent<Rigidbody>();
            ragDollEnemy = GetComponent<RagDollEnemy>();
        }
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
            ragDollEnemy.EnableRagdoll();
            //  rb.AddForce(new Vector3(Random.Range(-1f, 1f), 1f,0f) * enemyDamageForce, ForceMode.Impulse);
        }
    }


}