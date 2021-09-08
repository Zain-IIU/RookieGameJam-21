using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyDamage : MonoBehaviour
{  
    [SerializeField] private float enemyDamangeForceAmount = 7f;
    [SerializeField] private LayerMask enemyLayerMask;
    
    [SerializeField] private float enemyDamageForce = 20f;
    [SerializeField] private float forceAmount = 30f;

    [SerializeField] private GameObject enemyDeadFX;

    private bool isDestroyed;
    private bool isTrigger;

    private Rigidbody rb;
    private RagDollEnemy ragDollEnemy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ragDollEnemy = GetComponent<RagDollEnemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Debug.Log("Projectile");
            ragDollEnemy.EnableRagdoll();
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


/*if(other.gameObject.CompareTag("Projectile"))
      {
          Destroy(gameObject); 
          // other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
          Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, enemyLayerMask);

          foreach (var collider in colliders)
          {
              if (collider != null)
              {
                 rb.AddForce(new Vector3(Random.Range(-1f, 1f) * enemyDamangeForceAmount, enemyDamangeForceAmount, enemyDamangeForceAmount), ForceMode.Impulse);
              }
          }
      }
      
      if (other.gameObject.CompareTag("Player") && other.transform.localScale != Vector3.one)
      {
          // todo add crushing sound
          Instantiate(enemyDeadFX, transform.position, enemyDeadFX.transform.rotation);
          Destroy(gameObject);
      }

      if (!isDestroyed && other.gameObject.CompareTag("MiniPlayers"))
      {
          /*isDestroyed = true;
          Destroy(other.gameObject);
          Destroy(gameObject);#1#
      }*/