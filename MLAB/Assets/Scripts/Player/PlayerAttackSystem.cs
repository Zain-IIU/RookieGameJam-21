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
    static int speedPickUps;


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
        Debug.DrawRay(raypoint.position, raypoint.forward*rangeEnemyDistance,Color.red);
        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, rangeEnemyDistance, enemyMask))
        {
            if (hitInfo.collider != null && !runOnce)
            {
                runOnce = true;
                
               
                if (animationTrigger != "Speed")
                {
                    animator.SetTrigger(animationTrigger);
                }
               
                if (animationTrigger == "MagicAttack")
                {
                    Debug.Log(animationTrigger);
                    PlayerAnimationsHandler.instance.MageFellowAnimations("MagicAttack");
                }
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

    public int totalSpeed()
    {
        return speedPickUps;
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

    public void SetSpeedCount(int amount)
    {
        speedPickUps = amount;
    }
    
    //incrementing each power
    public void incrementPowers()
    {
        switch(animationTrigger)
        {
            case "MagicAttack":
                if(magicPickUps<3)
                     magicPickUps++;
                footDustFx.SetActive(false);
                break;
            case "SwordAttack":
                if(swordPickUps<3)
                    swordPickUps++;
                footDustFx.SetActive(true);
                break;
            case "GroundHammerAttack":
                if(hammerPickUps<3)
                   hammerPickUps++;
                footDustFx.SetActive(true);
                break;
            case "MageAttack":
                footDustFx.SetActive(true);
                break;
            case "Speed":
                speedPickUps++;
                footDustFx.SetActive(false);
                break;
        }
        Debug.Log(hammerPickUps + "     " + swordPickUps + "    " + magicPickUps);
    }


}
