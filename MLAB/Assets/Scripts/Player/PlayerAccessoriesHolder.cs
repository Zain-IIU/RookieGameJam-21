using DG.Tweening;
using UnityEngine;

public class PlayerAccessoriesHolder : MonoBehaviour
{
    public static PlayerAccessoriesHolder instance;
    
    [SerializeField] private PlayerAccessories playerAccessories;

    private PlayerAttackSystem playerAttackSystem;
    
    private void Awake()
    {
        instance = this;
        playerAttackSystem = GetComponent<PlayerAttackSystem>();
    }
    

    public void SetAccesories(PowerType powerType)
    {
        switch (powerType)
        {
            case PowerType.SizeAttack:
                playerAccessories.sizeItems[playerAttackSystem.GetPickupCount()].SetActive(true);
                break;
            
            case PowerType.MagicAttack:
                playerAccessories.magicitems[playerAttackSystem.GetPickupCount()].SetActive(true);
                AccessoryPositioningEffect(playerAccessories.magicitems[playerAttackSystem.GetPickupCount()].transform);
                playerAccessories.mageFellows[playerAttackSystem.GetPickupCount()].SetActive(true);
                break;

            case PowerType.SwordAttack:
                playerAccessories.swordItems[playerAttackSystem.GetPickupCount()].SetActive(true);
                AccessoryPositioningEffect(playerAccessories.swordItems[playerAttackSystem.GetPickupCount()].transform);
                break;

            case PowerType.GroundHammerAttack:
                playerAccessories.hammerItems[playerAttackSystem.GetPickupCount()].SetActive(true);
                AccessoryPositioningEffect(playerAccessories.hammerItems[playerAttackSystem.GetPickupCount()].transform);
                playerAccessories.hammerFX[playerAttackSystem.GetPickupCount()].SetActive(true);
                break;

            case PowerType.MultiplierAttack:
                playerAccessories.speedFellows[playerAttackSystem.GetPickupCount()].SetActive(true);
                playerAccessories.speedItems[playerAttackSystem.GetPickupCount()].SetActive(true);               
                break;
            
            case PowerType.MuscleAttack:
                playerAccessories.muscleItems[playerAttackSystem.GetPickupCount()].SetActive(true);
                AccessoryPositioningEffect(playerAccessories.muscleItems[playerAttackSystem.GetPickupCount()].transform);
                break;
        }
        ResetOtherAccessories(true,powerType);
    }
    void ResetOtherAccessories(bool toEnable,PowerType curPower)
    {
        for(int i=0;i<3;i++)
        {
            switch (curPower)
            {
                case PowerType.SizeAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    playerAccessories.muscleItems[i].SetActive(!toEnable);
                    break;

                
                case PowerType.MagicAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.muscleItems[i].SetActive(!toEnable);
                    break;
                case PowerType.SwordAttack:
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    playerAccessories.muscleItems[i].SetActive(!toEnable);
                    break;
                case PowerType.GroundHammerAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    playerAccessories.muscleItems[i].SetActive(!toEnable);
                    break;
                case PowerType.MultiplierAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    playerAccessories.muscleItems[i].SetActive(!toEnable);
                    break;
                
                case PowerType.MuscleAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    break;
             
            }
            
        }
    }

    public void ResetAllAcessories()
    {
        UIManager.instance.ResetPowerMeter();
        for (int i = 0; i < 3; i++)
        {
            playerAccessories.swordItems[i].SetActive(false);
            playerAccessories.hammerItems[i].SetActive(false);
            playerAccessories.magicitems[i].SetActive(false);
            playerAccessories.speedItems[i].SetActive(false);
            playerAccessories.speedFellows[i].SetActive(false);
            playerAccessories.mageFellows[i].SetActive(false);
            playerAccessories.muscleItems[i].SetActive(false);
        }
    }

    void AccessoryPositioningEffect(Transform itemPos)
    {
        Vector3 equippedPos =  itemPos.localPosition;
        itemPos.transform
            .DOLocalMove(equippedPos, 0.25f).From(new Vector3(10, -10f, 10f));
    }
}


