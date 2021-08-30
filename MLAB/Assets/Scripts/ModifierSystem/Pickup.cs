using System;
using DG.Tweening;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private float modifiedSpeed = 15f;
    
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
        KnifeThrow
    }
    public PowerType powerType;

    public GameObject pickupFx;

    public bool isConsumable;

    public Ease scaleEase;
    public float easeTimer;

    private Vector3 playerSize;
    private static float sizeVal = 0.15f;
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
                    playerSize += new Vector3(sizeVal, sizeVal, sizeVal);
                    playerSize.x = Mathf.Clamp(playerSize.x, 1f, 2f);
                    playerSize.y = Mathf.Clamp(playerSize.x, 1f, 2f);
                    playerSize.z = Mathf.Clamp(playerSize.x, 1f, 2f);
                    
                    playerModifier.transform.DOScale(playerSize, easeTimer).SetEase(scaleEase);
                    sizeVal += increment;
                    Debug.Log("Size" + sizeVal);
                }

                else if (modifierTypes == ModifierTypes.Speed)
                {
                    playerModifier.SetMoveSpeed(modifiedSpeed);
                    playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
                }
            }
            else if (isConsumable && modifierTypes == ModifierTypes.ConsumableModifier)
            {
                PlayerAttacking.instance.animationTrigger = powerType.ToString();
            }
        }
         
        
        //  pickupFx.SetActive(true);
        Destroy(gameObject);
    }
}
