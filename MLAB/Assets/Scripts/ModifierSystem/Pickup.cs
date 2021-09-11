using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;


public class Pickup : MonoBehaviour
{
    private PlayerMovement playerMovement;

    //Custom Class for Handling PlayerPowers
    public PowerType powerType;

    public Ease scaleEase;

    public float easeTimer;

    private Vector3 playerSize;

    private static float sizeVal = 1f;
    private static float speedVal;
   
    public float increment;

    private Vector3 lastSize;
    
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        lastSize = playerSize = playerMovement.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerMovement playerModifier))
        {   
            PlayerAttackSystem.instance.SetCurPower(powerType);
            
            PlayerAttackSystem.instance.SetPowerTrailEffect();

            PlayerAccessoriesHolder.instance.SetAccesories(powerType);
            
            //normalizing sizeVal
            if (powerType == PowerType.SizeAttack)
            {
                if (sizeVal > 2.5)
                    sizeVal = 1;

                playerModifier.SetMoveSpeed(10);
                sizeVal += increment;
                playerSize = new Vector3(sizeVal, sizeVal, sizeVal);
                playerSize.x = Mathf.Clamp(playerSize.x, 1f, 2.5f);
                playerSize.y = Mathf.Clamp(playerSize.x, 1f, 2.5f);
                playerSize.z = Mathf.Clamp(playerSize.x, 1f, 2.5f);

                  
                playerModifier.transform.DOScale(playerSize, easeTimer).From(lastSize).SetEase(scaleEase);
                lastSize = playerSize;
            }
            
            else
            {
                playerModifier.transform.DOScale(Vector3.one, easeTimer).SetEase(scaleEase);
                playerModifier.SetMoveSpeed(10);
            }
            
            Destroy(gameObject);
        }
        // todo add better pickups
        // pickupFx.SetActive(true);
     
      
      
    }
}
