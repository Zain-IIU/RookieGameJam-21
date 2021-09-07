using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    
    // todo attach hit effect later 
    
    [SerializeField] private float forceAmount = 30f;
    [SerializeField] private float enemyDamangeForceAmount = 7f;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float timeToDestroyBullet = 1.5f;
    private float timeCounter;
    
    private Rigidbody rigidbody;

   
    public bool isEnemyBullet;

    enum ProjectileDirection
    {
        right,
        left,
        forward,
        back,
        up
    }

    [SerializeField] private ProjectileDirection projectileDirection;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void AddProjectileForce(Vector3 direction)
    {
        rigidbody.AddForce(direction * forceAmount, ForceMode.Impulse);
    }

    void Start()
    {
        timeCounter = timeToDestroyBullet;

        switch (projectileDirection)
        {
            case ProjectileDirection.forward:
                AddProjectileForce(transform.forward);
                break;
            case ProjectileDirection.back:
                AddProjectileForce(-transform.forward);
                break;
            case ProjectileDirection.right:
                AddProjectileForce(transform.right);
                break;
            case ProjectileDirection.left:
                AddProjectileForce(-transform.right);
                break;
            case ProjectileDirection.up:
                AddProjectileForce(transform.up);
                break;
            
        }
       
    }

    private void Update()
    {
        timeCounter -= Time.deltaTime;

        if (timeCounter < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnemyBullet) return;

        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // todo attach player damage stuff later
            Debug.Log("Player got hit");
            other.attachedRigidbody.AddTorque(new Vector3(1,1,1) * 20f, ForceMode.Impulse);
            other.attachedRigidbody.AddForce(new Vector3(0,1,1) * 10f, ForceMode.Impulse);
        }
        
        if (isEnemyBullet) return;
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
           // other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
           Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, enemyLayerMask);

           foreach (var collider in colliders)
           {
               if (collider != null)
               {
                   collider.attachedRigidbody.AddForce(new Vector3(Random.Range(-1f, 1f) * enemyDamangeForceAmount, enemyDamangeForceAmount, enemyDamangeForceAmount), ForceMode.Impulse);
               }
              
           }
        }
    }
}
