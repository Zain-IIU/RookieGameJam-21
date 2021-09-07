using System;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    
    enum EnemyType
    {
        CowardEnemy,
        Shooters
    }

    [SerializeField] private EnemyType enemyType;
    
    [SerializeField] private GameObject enemyDeadFX;
    
    [SerializeField] private float enemyRunSpeed = 6f;
    [SerializeField] private float minDistanceRange;
    
    private Transform playerTransform;
    private Animator animator;
    private Rigidbody rb;

    private bool isDestroyed;

    [SerializeField] private float timeToAttack = 3f;
    private float timerCounter;
    
    
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        timerCounter = timeToAttack;
    }

    private void Start()
    {
        GameManager.instance.OnGameStart += OnGameStart;
    }

    void OnGameStart()
    {
        switch (enemyType)
        {
            case EnemyType.CowardEnemy:
                animator.SetTrigger("CowardEnemy");
                break;
            
            case EnemyType.Shooters:
      
                 animator.SetTrigger("ShooterEnemy");
                 break;
        }
    }

    void Update()
    {
        switch (enemyType)
        {
            case EnemyType.CowardEnemy:
                
                if (playerTransform.localScale != Vector3.one && DistanceCheck(minDistanceRange))
                {
                    animator.SetTrigger("StartRun");
                    transform.Translate(Vector3.forward * (enemyRunSpeed * Time.deltaTime));
                }
                break;
            
            case EnemyType.Shooters:
                if (DistanceCheck(minDistanceRange))
                {
                    timerCounter -= Time.deltaTime;
                    if (timerCounter < 0f)
                    {
                        timerCounter = timeToAttack;
                        animator.SetTrigger("Shoot");
                    }
                } 
                else if (playerTransform.localScale != Vector3.one && DistanceCheck(6f))
                {
                    animator.SetTrigger("StartRun");
                }
                break;
        }
    
    }

    bool DistanceCheck(float minRangeDist)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < minRangeDist)
        {
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
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
            Destroy(gameObject);*/
        }
    }
}
