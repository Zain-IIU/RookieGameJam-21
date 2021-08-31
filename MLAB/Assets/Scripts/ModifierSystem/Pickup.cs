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
        GroundAttack,
        KnifeThrow,
        MagicAttack
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
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        playerSize = playerMovement.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerModifier))
        {
            if (!isConsumable)
            {
                if (modifierTypes == ModifierTypes.Size)
                {
                    PlayerAttacking.instance.hasConsumed = false;
                    playerModifier.SetMoveSpeed(10);
                    sizeVal += increment;
                    playerSize = new Vector3(sizeVal, sizeVal, sizeVal);
                    playerSize.x = Mathf.Clamp(playerSize.x, 1f, 2f);
                    playerSize.y = Mathf.Clamp(playerSize.x, 1f, 2f);
                    playerSize.z = Mathf.Clamp(playerSize.x, 1f, 2f);
                    
                    playerModifier.transform.DOScale(playerSize, easeTimer).SetEase(scaleEase);
                   
                    
                 }

                else if (modifierTypes == ModifierTypes.Speed)
                {
                    PlayerAttacking.instance.hasConsumed = false;
                    speedVal = playerModifier.GetMoveSpeed();
                    speedVal += modifiedSpeed;
                    Debug.Log(speedVal);
                    speedVal = Mathf.Clamp(speedVal, 10, 15);
                    playerModifier.SetMoveSpeed(speedVal);

                    playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
                }
                PickUpManager.instance.setPower(modifierTypes.ToString());
            }
            else if (isConsumable && modifierTypes == ModifierTypes.ConsumableModifier)
            {
                playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
                playerModifier.SetMoveSpeed(10);

                PlayerAttacking.instance.hasConsumed = true;
                PlayerAttacking.instance.animationTrigger = powerType.ToString();
                Debug.Log("Player Power Attack");
            }
        }
         
        
        pickupFx.SetActive(true);
        Destroy(gameObject);
    }
}
