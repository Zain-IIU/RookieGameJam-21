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
            case EnemyType.BarrelThrower:
                animator.SetTrigger("ThrowerEnemy");
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
                EnemyAttack("Shoot");
                break;
            case EnemyType.BarrelThrower:
                EnemyAttack("Throw");
                break;
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

  
}
