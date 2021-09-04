using UnityEngine;

public class PlayerAccessoriesHolder : MonoBehaviour
{
    public static PlayerAccessoriesHolder instance;

    
    [SerializeField] private PlayerAccessories playerAccessories;

    private void Awake()
    {
        instance = this;
    }
    

    public void SetAccesories(PowerType powerType)
    {
        ResetOtherAccessories(true,powerType);
        switch (powerType)
        {
            case PowerType.MagicAttack:
              
                playerAccessories.magicitems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.mageFellows[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;

            case PowerType.SwordAttack:
                playerAccessories.swordItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;

            case PowerType.GroundHammerAttack:
                playerAccessories.hammerItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                
                break;

            case PowerType.SpeedAttack:
                playerAccessories.speedFellows[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.speedItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);               
                break;

            case PowerType.SizeAttack:
                playerAccessories.sizeItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;

        }
        
    }
    void ResetOtherAccessories(bool toEnable,PowerType curPower)
    {
        for(int i=0;i<3;i++)
        {
            switch (curPower)
            {
                case PowerType.MagicAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                   
                    break;
                case PowerType.SwordAttack:
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    break;
                case PowerType.GroundHammerAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.speedItems[i].SetActive(!toEnable);
                    playerAccessories.speedFellows[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    break;
                case PowerType.SpeedAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    break;
                case PowerType.SizeAttack:
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
}
