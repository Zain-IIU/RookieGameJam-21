using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyTest : MonoBehaviour
{
    [SerializeField] private GameObject enemyDeadFX;
    
    [SerializeField] private float enemyRunSpeed = 6f;
    [SerializeField] private float distanceToRunAway;
    
    private float distanceToPlayer;
    
    private Transform playerTransform;
    private Animator animator;
    private Rigidbody rb;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (playerTransform.localScale != Vector3.one)
        {
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < distanceToRunAway)
            {
                animator.SetTrigger("StartRun");
                transform.Translate(Vector3.forward * (enemyRunSpeed * Time.deltaTime));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.transform.localScale != Vector3.one)
        {
            // todo add crushing sound
            Instantiate(enemyDeadFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            rb.AddForce(new Vector3(Random.Range(-1f, 1f) * 5f, 5f, 5f), ForceMode.Impulse);
        }
    }
}