using System;
using System.Collections;
using UnityEngine;

public class PlayerAttackSystem : MonoBehaviour
{
    public static PlayerAttackSystem instance;

    [SerializeField] private Transform raypoint;
    [SerializeField] private float rangeEnemyDistance;

    [SerializeField] private GameObject footDustFx;
    
    [SerializeField] private float singleEnemyDistance;
    
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask singleEnemyMask;
    
    public string animationTrigger;

    private Animator animator;
    public Animator[] mageAnimators;
    
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
            if (hitInfo.collider != null && !runOnce)
            {
                runOnce = true;
                // hasConsumed = false;
                animator.SetTrigger(animationTrigger);
            }
        }
        
        
        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, singleEnemyDistance, singleEnemyMask))
        {
            if (hitInfo.collider != null && !runOnce)
            {
                runOnce = true;
                // todo setup all normal attacks in a nice way
                animator.SetTrigger("SwordNormalAttack");
               // animator.SetTrigger("HammerProjectileAttack");

               /*if (animationTrigger is "MagicAttack")
               {
                   animator.SetTrigger("MageProjectileAttack");

                   foreach (var mageAnim in mageAnimators)
                   {
                       mageAnim.SetTrigger("MageProjectileAttack");
                   }
               }*/
            }
        }

        /*if (animationTrigger == "MagicAttack")
        {
            singleEnemyDistance = 12f;
            animator.SetBool("MageRun", true);
            footDustFx.SetActive(false);
        }
        else
        {
            singleEnemyDistance = 5f;
            footDustFx.SetActive(true);
            animator.SetBool("MageRun", false);
        }*/
    }


}
