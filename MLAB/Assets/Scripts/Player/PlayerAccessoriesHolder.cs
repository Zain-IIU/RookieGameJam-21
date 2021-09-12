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
        switch (powerType)
        {
            case PowerType.SizeAttack:
                playerAccessories.sizeItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;
            
            case PowerType.MagicAttack:
                playerAccessories.magicitems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.mageFellows[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;

            case PowerType.SwordAttack:
                playerAccessories.swordItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;

            case PowerType.GroundHammerAttack:
                playerAccessories.hammerItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.hammerFX[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                break;

            case PowerType.MultiplierAttack:
                playerAccessories.speedFellows[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.speedItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);               
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
                    break;

                
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
                case PowerType.MultiplierAttack:
                    playerAccessories.swordItems[i].SetActive(!toEnable);
                    playerAccessories.hammerItems[i].SetActive(!toEnable);
                    playerAccessories.magicitems[i].SetActive(!toEnable);
                    playerAccessories.mageFellows[i].SetActive(!toEnable);
                    break;
             
            }
            
        }
    }

    public void ResetAllAcessories()
    {
        for (int i = 0; i < 3; i++)
        {
            playerAccessories.swordItems[i].SetActive(false);
            playerAccessories.hammerItems[i].SetActive(false);
            playerAccessories.magicitems[i].SetActive(false);
            playerAccessories.speedItems[i].SetActive(false);
            playerAccessories.speedFellows[i].SetActive(false);
            playerAccessories.mageFellows[i].SetActive(false);
        }
    }
}


