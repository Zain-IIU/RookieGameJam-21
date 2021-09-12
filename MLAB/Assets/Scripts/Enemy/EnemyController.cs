using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour
{
    
    enum EnemyType
    {
        CowardEnemy,
        Shooters,
        BarrelThrower
    }

    [SerializeField] private EnemyType enemyType;
    [SerializeField] bool isRagdoll;
   
    [SerializeField] private float enemyRunSpeed = 6f;
 
    [SerializeField] private float minDistanceRange;
    
    private Transform playerTransform;
    private Animator animator;
   
   
    [SerializeField] private float timeToAttack = 3f;
    private float timerCounter;
    
    bool isGiantPlayer;
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
       
        timerCounter = timeToAttack;
    }

    private void Start()
    {
       
        switch (enemyType)
        {
            case EnemyType.CowardEnemy:
                animator.SetTrigger("CowardEnemy");
                break;
            
            case EnemyType.Shooters:
                animator.SetTrigger("ShooterEnemy");
                break;

            case EnemyType.BarrelThrower:
                animator.SetTrigger("ThrowerEnemy");
                break;
        }
    }

    /*
    void OnGameStart()
    {
        animator.SetTrigger("StartGame");

    }*/

    void Update()
    {
        if (!GameManager.instance.isGameStarted) return;
        
        switch (enemyType)
        {
            case EnemyType.CowardEnemy:

                if (playerTransform.localScale != Vector3.one && DistanceCheck(minDistanceRange))
                {
                    animator.SetTrigger("StartRun");

                    if (isRagdoll)
                    {
                        isRagdoll = false;
                        transform.DORotate(new Vector3(0, 0, 0), 0.25f);
                    }

                    transform.Translate(Vector3.forward * (enemyRunSpeed * Time.deltaTime));
                }
                else
                {
                    animator.SetTrigger("StopRun");
                }
                break;

            case EnemyType.Shooters:
                EnemyAttack("Shoot");
                break;
            case EnemyType.BarrelThrower:
                EnemyAttack("Throw");
                break;
        }

        if (transform.position.z + 3f< playerTransform.transform.position.z)
        {
            Destroy(gameObject);
        }
        
    }
    
    void EnemyAttack(string trigger)
    {
        if (!isGiantPlayer && DistanceCheck(minDistanceRange))
        {
            timerCounter -= Time.deltaTime;
            if (timerCounter < 0f)
            {
                timerCounter = timeToAttack;
                animator.SetTrigger(trigger);
            }
        }

        if (playerTransform.localScale != Vector3.one && DistanceCheck(10f))
        {
            isGiantPlayer = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
           
            animator.SetTrigger("StartRun");
            transform.Translate(Vector3.forward * (enemyRunSpeed * Time.deltaTime));
        }
        else
        {
            animator.SetTrigger("StopRun");
        }
    }

    bool DistanceCheck(float minRangeDist)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= minRangeDist)
        {
            return true;
        }
        return false;
    }

  
}
