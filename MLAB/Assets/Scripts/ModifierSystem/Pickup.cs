using System;
using DG.Tweening;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    private PlayerMovement playerMovement;
   
    public enum ModifierTypes
    {
        Size,
        Speed,
        ConsumableModifier,
    }

    public ModifierTypes modifierTypes;

    
    public enum PowerType
    {
        Null,
        GroundHammerAttack,
        MageGroundAttack,
        MagicAttack,
        SwordAttack
    }
    
    public PowerType powerType;

    public GameObject pickupFx;

    public bool isConsumable;

    public Ease scaleEase;
    public float easeTimer;

    private Vector3 playerSize;

    private static float sizeVal = 1f;
    private static float speedVal;
    [SerializeField] private float modifiedSpeed;

    public float increment;


    
    //[Header("Player Accessory")] 
    //[SerializeField] private GameObject[] playerWaeponType;

    [SerializeField] private GameObject lightingTrail;
    [SerializeField] private GameObject footTrail;
    
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        
    }

    private void Start()
    {
        playerSize = playerMovement.transform.localScale;

        if (powerType != PowerType.Null && modifierTypes==ModifierTypes.ConsumableModifier)
        {
            isConsumable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.TryGetComponent(out PlayerMovement playerModifier))
        {
            Debug.Log("Collision");
            //normalizing sizeVal
            if (sizeVal > 2)
                sizeVal = 1;

            if (!isConsumable)
            {
                if (modifierTypes == ModifierTypes.Size)
                {
                    
                    PlayerAttackSystem.instance.hasConsumed = false;
                    playerModifier.SetMoveSpeed(10);
                    sizeVal += increment;
                    playerSize = new Vector3(sizeVal, sizeVal, sizeVal);
                    playerSize.x = Mathf.Clamp(playerSize.x, 1f, 2f);
                    playerSize.y = Mathf.Clamp(playerSize.x, 1f, 2f);
                    playerSize.z = Mathf.Clamp(playerSize.x, 1f, 2f);
                    Debug.Log(playerSize+ "    "+ sizeVal);
                    footTrail.SetActive(true);
                    lightingTrail.SetActive(false);
                    playerModifier.transform.DOScale(playerSize, easeTimer).SetEase(scaleEase);
                    
                    PlayerAttackSystem.instance.animationTrigger = "";
                }

                else if (modifierTypes == ModifierTypes.Speed)
                {
                    PlayerAttackSystem.instance.hasConsumed = false;
                    speedVal = playerModifier.GetMoveSpeed();
                    speedVal += modifiedSpeed;
                    speedVal = Mathf.Clamp(speedVal, 10, 15);
                    playerModifier.SetMoveSpeed(speedVal);
                    
                    footTrail.SetActive(false);
                    lightingTrail.SetActive(true);
                    playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
                    
                    PlayerAttackSystem.instance.animationTrigger = "";
                }

                PlayerAccessoriesHolder.instance.SetAccesories(modifierTypes.ToString());

                PickUpManager.instance.SetPower(modifierTypes.ToString());
                Debug.Log("Speed or Size");
            }
            else if (isConsumable && modifierTypes == ModifierTypes.ConsumableModifier)
            {
                Debug.Log("Power Picked");
                playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
                playerModifier.SetMoveSpeed(10);

                //foreach (var playerWeapon in playerWaeponType)
                //{
                //    if (playerWeapon.name == powerType.ToString())
                //    {
                //        playerWeapon.SetActive(true);
                //    }
                //    else
                //    {
                //        playerWeapon.SetActive(false);
                //    }
                //}
                
                footTrail.SetActive(true);
                lightingTrail.SetActive(false);
                
                PlayerAttackSystem.instance.hasConsumed = true;
                PlayerAttackSystem.instance.animationTrigger = powerType.ToString();
                PlayerAccessoriesHolder.instance.SetAccesories(powerType.ToString());
                PlayerAttackSystem.instance.incrementPowers();
                
                
            }
        }
        // todo add better pickups
        // pickupFx.SetActive(true);
        Destroy(gameObject);
    }
}

