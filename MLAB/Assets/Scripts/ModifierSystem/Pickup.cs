using System;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttackSystem playerAttackSystem;
    
    //Custom Class for Handling PlayerPowers
    public PowerType powerType;

    public Ease scaleEase;
    public float easeTimer;

    [SerializeField] Transform playerMusleSpineSize;
    private static float musclePlayerSize = 1f;
    [SerializeField] private float musclePlayeIncrement = 0.3f;
    
    private Vector3 playerSize;
    private Vector3 lastSize;

    private static float sizeVal = 1f;
   
    public float increment;

   
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMusleSpineSize == null)
        {
            playerMusleSpineSize = GameObject.FindGameObjectWithTag("PlayerSpine").transform;
        }
    }
    

    private void Start()
    {
        playerAttackSystem = PlayerAttackSystem.instance;
        lastSize = playerSize = playerMovement.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerModifier))
        {   
            playerAttackSystem.SetCurPower(powerType);
            playerAttackSystem.SetEffectAndRayDistance();
            PlayerAccessoriesHolder.instance.SetAccesories(powerType);
            
            //normalizing sizeVal
            if (powerType == PowerType.SizeAttack)
            {
                SizeAttackPickup(playerModifier);
            }
            
            else if (powerType == PowerType.MuscleAttack)
            {
                MuscleAttackPickup();
            }
            
            else if (powerType == PowerType.Timer)
            {
                UIManager.instance.SetTimerClockValue();
                //Time.timeScale = 0.25f;
               // Time.fixedDeltaTime = Time.timeScale * 0.02f;
               // playerModifier.SetMoveSpeed(40f);
               
            }
            
            else
            {
                ResetPlayer(playerModifier);
            }
            
            Destroy(gameObject);
        }
    }

    private void SizeAttackPickup(PlayerMovement playerModifier)
    {
      //  ResetTimer();
        if (sizeVal > 2.5)
            sizeVal = 1;

        playerModifier.SetMoveSpeed(10f);
        sizeVal += increment;
        playerSize = new Vector3(sizeVal, sizeVal, sizeVal);
        playerSize.x = Mathf.Clamp(playerSize.x, 1f, 2.5f);
        playerSize.y = Mathf.Clamp(playerSize.x, 1f, 2.5f);
        playerSize.z = Mathf.Clamp(playerSize.x, 1f, 2.5f);


        playerModifier.transform.DOScale(playerSize, easeTimer).From(lastSize).SetEase(scaleEase);

        if (playerMusleSpineSize.localScale != Vector3.one)
        {
            playerMusleSpineSize.DOScale(1f, easeTimer).SetEase(scaleEase);
        }

        lastSize = playerSize;
    }

    private void ResetPlayer(PlayerMovement playerModifier)
    {
       //  ResetTimer();
        if (playerMusleSpineSize.localScale != Vector3.one)
        {
            playerMusleSpineSize.DOScale(1f, easeTimer).SetEase(scaleEase);
        }
        playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
        playerModifier.SetMoveSpeed(10f);
    }

    private void MuscleAttackPickup()
    {
        // ResetTimer();
        musclePlayerSize += musclePlayeIncrement;
        playerMusleSpineSize.DOScale(musclePlayerSize, easeTimer).SetEase(scaleEase);
    }

    private void ResetTimer()
    {
        /*if (Time.timeScale < 1f)
        {
            playerMovement.SetMoveSpeed(10f);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            UIManager.instance.timerClock.gameObject.SetActive(false);
        }*/
    }
}
