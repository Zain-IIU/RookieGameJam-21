using System.Runtime.CompilerServices;
using UnityEngine;

 public class PlayerAttackSystem : MonoBehaviour
{
    public static PlayerAttackSystem instance;

    [SerializeField] private Transform raypoint;
    [SerializeField] private float BossEnemyDistance;

    [SerializeField] private GameObject footDustFx;
    [SerializeField] private GameObject lightningFootFx;
    
    [SerializeField] private float singleEnemyDistance;
    
    [SerializeField] private LayerMask bossMask;
    [SerializeField] private LayerMask enemyMask;
    
    private Animator animator;
    
    public bool runOnce;

    private static int pickupCount;

    [SerializeField] private GameObject magePickupEffect;
    
    //custom class for handling curPower State
    
   public PowerType curPower;

   private bool powerAttack;
    private void Awake()
    {
        instance = this;
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


    private PowerType updatePower;
    
    private void Update()
    {
        
        Debug.Log(runOnce);
        // todo check previous power if its not equal then assigne cur power
        if (curPower != updatePower)
        {
            PlayerAnimationsHandler.instance.SetPlayerState(curPower);
            updatePower = curPower;
        }
        
        
        RaycastHit hitInfo;

        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, BossEnemyDistance, bossMask))
        {
            if (hitInfo.collider != null && !runOnce && !powerAttack)
            {
                powerAttack = true;
                runOnce = true;
                animator.SetTrigger(curPower.ToString());
                
                if (curPower.ToString() == "MagicAttack")
                {
                    PlayerAnimationsHandler.instance.MageFellowAnimations(curPower.ToString());
                }
            }
        }
        
        
        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, singleEnemyDistance, enemyMask))
        {
            if (hitInfo.collider != null && !runOnce  && curPower.ToString() != "SizeAttack")
            {
                runOnce = true;
                //````````
                PlayerAnimationsHandler.instance.SetTransitions(curPower);
                //````````
            }
        }
       
    }


    // todo : will check it later
    
    public void SetCurPower(PowerType newPower)
    {
        if (curPower != newPower) 
        {
            pickupCount = 0;
        }
        else
        {
            pickupCount++;
        }

        curPower = newPower;
       
    }

    
    //incrementing each power
    public void SetPowerTrailEffect()
    {
        switch(curPower)
        {
            case PowerType.MagicAttack:
                magePickupEffect.SetActive(true);
                SetRaycastDistance(15f);
                EnableFootTrailEffects(false, false);
                break;
            case PowerType.SwordAttack:
                SetRaycastDistance(6f);
                EnableFootTrailEffects(true, false);
                break;
            case PowerType.GroundHammerAttack:
                SetRaycastDistance(13f);
                EnableFootTrailEffects(true, false);
                break;
            case PowerType.MultiplierAttack:
                SetRaycastDistance(7f);
                EnableFootTrailEffects(false, false);
                break;
            
            case PowerType.SizeAttack:
                magePickupEffect.SetActive(true);
                SetRaycastDistance(7f);
                EnableFootTrailEffects(false, false);
                break;
        }
    }

    public int GetPickupCount()
    {
        return pickupCount;
    }
    
    public void EnableFootTrailEffects(bool isDustEnable, bool isLightingEnable)
    {
        footDustFx.SetActive(isDustEnable);
        lightningFootFx.SetActive(isLightingEnable);
    }


    private void SetRaycastDistance(float newDistance)
    {
        singleEnemyDistance = newDistance;
    }
    

}
