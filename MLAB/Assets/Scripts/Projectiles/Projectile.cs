using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    // todo attach hit effect later 
    
    [SerializeField] private float forceAmount = 30f;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float timeToDestroyBullet = 1.5f;
    private float timeCounter;
    
    private Rigidbody rigidbody;

    public bool isInverted;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        timeCounter = timeToDestroyBullet;

        if (!isInverted)
        {
            rigidbody.AddForce(transform.forward * forceAmount, ForceMode.Impulse);
        }
        else
        {
            rigidbody.AddForce(-transform.forward * forceAmount, ForceMode.Impulse);
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
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
           // other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
           Collider[] colliders = Physics.OverlapSphere(transform.position, 10f, enemyLayerMask);

           foreach (var collider in colliders)
           {
               collider.attachedRigidbody.AddForce(new Vector3(Random.Range(-1f, 1f) * 4f, 4f, 4f), ForceMode.Impulse);;
           }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // todo attach player damage stuff later
            Debug.Log("Player got hit");
        }
    }
}
