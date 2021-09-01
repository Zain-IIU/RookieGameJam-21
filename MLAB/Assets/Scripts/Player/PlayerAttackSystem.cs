using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public static PlayerAttackSystem instance;

    [SerializeField] private Transform raypoint;
    [SerializeField] private float rangeEnemyDistance;
    
    [SerializeField] private float singleEnemyDistance;
    
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask singleEnemyMask;
    
    public string animationTrigger;

    private Animator animator;

    public static bool runOnce;

    public bool hasConsumed;
    
    private void Awake()
    {
        instance = this;
        hasConsumed = false;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
       animator.SetBool("hasStarted", true);
    }

    private void Update()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, rangeEnemyDistance, enemyMask))
        {
            if (hitInfo.collider != null && !runOnce && hasConsumed)
            {
                runOnce = true;
                hasConsumed = false;
                animator.SetTrigger(animationTrigger);
               
            }
        }
        
       
 
    }


}
