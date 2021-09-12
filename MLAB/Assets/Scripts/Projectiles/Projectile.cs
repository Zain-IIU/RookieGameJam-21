using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    
    // todo attach hit effect later 
    
    [SerializeField] private float forceAmount = 30f;
   
    [SerializeField] private float timeToDestroyBullet = 1.5f;
    private float timeCounter;
    
    private Rigidbody RB;
    
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
        RB = GetComponent<Rigidbody>();
    }

    void AddProjectileForce(Vector3 direction)
    {
        RB.AddForce(direction * forceAmount, ForceMode.Impulse);
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            other.gameObject.GetComponent<RagDollEnemy>().EnableRagdoll();
        }
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
