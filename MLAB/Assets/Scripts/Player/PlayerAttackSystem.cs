using UnityEngine;

 public class PlayerAttackSystem : MonoBehaviour
{
    public static PlayerAttackSystem instance;

    [SerializeField] private Transform raypoint;
    [SerializeField] private float BossEnemyDistance;

    [SerializeField] private float singleEnemyDistance;
    
    [SerializeField] private LayerMask bossMask;
    [SerializeField] private LayerMask enemyMask;
    
    private Animator animator;

    public static bool runOnce;
    
   // public bool runOnce;

    private static int pickupCount;

    [SerializeField] private GameObject magePickupEffect;
    [SerializeField] private GameObject invincibleEffect;
    
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
       animator.SetBool(HasStarted, true);
    }


    private PowerType updatePower;
    private static readonly int HasStarted = Animator.StringToHash("hasStarted");

    private void Update()
    {
        
        // todo check previous power if its not equal then assigne cur power
        if (curPower != updatePower)
        {
            PlayerAnimationsHandler.instance.SetPlayerState(curPower);
            updatePower = curPower;
        }
        
        
        RaycastHit hitInfo;

        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, BossEnemyDistance, bossMask))
        {
            if (hitInfo.collider != null  && !powerAttack)
            {
                powerAttack = true;
               // runOnce = true;
                animator.SetTrigger(curPower.ToString());
                
                if (curPower.ToString() == "MagicAttack")
                {
                    PlayerAnimationsHandler.instance.MageFellowAnimations(curPower.ToString(),true);
                }
            }
        }
        
        
        if (!runOnce && Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, singleEnemyDistance, enemyMask))
        {
            if (hitInfo.collider != null && curPower.ToString() != "SizeAttack")
            {
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

    public PowerType GetCurrentPower()
    {
        return curPower;
    }

    
    //incrementing each power
    public void SetEffectAndRayDistance()
    {
        switch(curPower)
        {
            case PowerType.MagicAttack:
                magePickupEffect.SetActive(true);
                SetRaycastDistance(15f);
                break;
            case PowerType.SwordAttack:
                SetRaycastDistance(4f);
                break;
            case PowerType.GroundHammerAttack:
                SetRaycastDistance(13f);
                break;
            case PowerType.MultiplierAttack:
                SetRaycastDistance(7f);
                break;
            
            case PowerType.SizeAttack:
                magePickupEffect.SetActive(true);
                SetRaycastDistance(7f);
                break;
            case PowerType.MuscleAttack:
                magePickupEffect.SetActive(true);
                break;
            case PowerType.Timer:
                invincibleEffect.SetActive(true);
                break;
        }
    }

    public int GetPickupCount()
    {
        UIManager.instance.SetPowerMeter(pickupCount + 1f);
        return pickupCount;
    }

    private void SetRaycastDistance(float newDistance)
    {
        singleEnemyDistance = newDistance;
    }
    

}
