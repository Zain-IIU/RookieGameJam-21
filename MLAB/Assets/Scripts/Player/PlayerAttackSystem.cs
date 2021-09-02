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
    
    
    public static bool runOnce;

    public bool hasConsumed;

    static int  magicPickUps=0;
    static int  swordPickUps=0;
    static int hammerPickUps =0;


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

        PlayerAnimationsHandler.instance.SetPlayerState(animationTrigger);


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
                //````````
                PlayerAnimationsHandler.instance.SetTransitions(animationTrigger);
                //````````



                // todo setup all normal attacks in a nice way
                //animator.SetTrigger("SwordNormalAttack");
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

    //for Counting total Attacks for accesorires placement as per need
    public int totalMagic()
    {
        return magicPickUps;
    }
    public int totalHammer()
    {
        return hammerPickUps;
    }
    public int totalSword()
    {
        return swordPickUps;
    }
    
    //reseting them when player picks other power
    public void SetMagicCount(int amount)
    {
        magicPickUps = amount;
    }
    public void SetHammerCount(int amount)
    {
        hammerPickUps = amount;
    }
    public void SetSwordCount(int amount)
    {
        swordPickUps = amount;
    }
    //incrementing each power
    public void incrementPowers()
    {
        switch(animationTrigger)
        {
            case "MagicAttack":
                if(magicPickUps<3)
                     magicPickUps++;
                break;
            case "SwordAttack":
                if(swordPickUps<3)
                    swordPickUps++;
                break;
            case "GroundHammerAttack":
                if(hammerPickUps<3)
                   hammerPickUps++;
                break;
            case "MageAttack":

                break;
        }
        Debug.Log(hammerPickUps + "     " + swordPickUps + "    " + magicPickUps);
    }


}
