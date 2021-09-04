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
            case PowerType.MagicAttack:
              
                playerAccessories.magicitems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.mageFellows[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                for(int i=0;i<3;i++)
                {
                    playerAccessories.speedItems[i].SetActive(false);
                    playerAccessories.swordItems[i].SetActive(false);
                    playerAccessories.hammerItems[i].SetActive(false);
                    playerAccessories.speedFellows[i].SetActive(false);
                    playerAccessories.sizeItems[i].SetActive(false);
                }
                break;
            
            case PowerType.SwordAttack:
                playerAccessories.swordItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    playerAccessories.magicitems[i].SetActive(false);
                    playerAccessories.hammerItems[i].SetActive(false);
                    playerAccessories.sizeItems[i].SetActive(false);
                    playerAccessories.speedItems[i].SetActive(false);

                    playerAccessories.mageFellows[i].SetActive(false);
                    playerAccessories.speedFellows[i].SetActive(false);
                }
              
                break;
            case PowerType.GroundHammerAttack:
                playerAccessories.hammerItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    playerAccessories.magicitems[i].SetActive(false);
                    playerAccessories.swordItems[i].SetActive(false);
                    playerAccessories.sizeItems[i].SetActive(false);
                    playerAccessories.speedItems[i].SetActive(false);

                    playerAccessories.mageFellows[i].SetActive(false);
                    playerAccessories.speedFellows[i].SetActive(false);
                }
                break;
            
            /*case :
                //todo finding accesories for both size and speed
                break;*/
            
            case PowerType.SpeedAttack:
                playerAccessories.speedFellows[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                playerAccessories.speedItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                for(int i=0;i<3;i++)
                {
                    playerAccessories.swordItems[i].SetActive(false);
                    playerAccessories.hammerItems[i].SetActive(false);
                    playerAccessories.magicitems[i].SetActive(false);
                    playerAccessories.sizeItems[i].SetActive(false);
                    playerAccessories.mageFellows[i].SetActive(false);
                }
               
               
                break;
            
            case PowerType.SizeAttack:
                playerAccessories.sizeItems[PlayerAttackSystem.instance.GetPickupCount()].SetActive(true);
                for(int i=0;i<3;i++)
                {
                    playerAccessories.swordItems[i].SetActive(false);
                    playerAccessories.hammerItems[i].SetActive(false);
                    playerAccessories.magicitems[i].SetActive(false);
                    playerAccessories.speedItems[i].SetActive(false);
                    
                    playerAccessories.speedFellows[i].SetActive(false);
                    playerAccessories.mageFellows[i].SetActive(false);
                }
                break;
        }
        
    }
}
