using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAccessoriesHolder : MonoBehaviour
{
    public static PlayerAccessoriesHolder instance;

    [Header("Magic Attack")]
    [SerializeField]
    GameObject[] magicitems;

    [Header("Sword Attack")]
    [SerializeField]
    GameObject[] swordItems;

    [Header("Hammer Attack")]
    [SerializeField]
    GameObject[] hammerItems;

    [Header("Speed Attack")]
    [SerializeField]
    GameObject[] speedItems;

    [Header("Size Attack")]
    [SerializeField]
    GameObject[] sizeItems;

    [Header("Mage Fellow")] 
    [SerializeField]
    private GameObject[] mageFellows;
    
    [Header("Speed Fellow")] 
    [SerializeField]
    private GameObject[] speedFellows;
    

    private void Awake()
    {
        instance = this;
    }
    

    public void SetAccesories(string powerType)
    {
        switch (powerType)
        {
            case "MagicAttack":
                magicitems[PlayerAttackSystem.instance.totalMagic()].SetActive(true);
                mageFellows[PlayerAttackSystem.instance.totalMagic()].SetActive(true);
                for(int i=0;i<3;i++)
                {
                    swordItems[i].SetActive(false);
                    hammerItems[i].SetActive(false);
                    speedFellows[i].SetActive(false);
                }
                PlayerAttackSystem.instance.SetHammerCount(0);
                PlayerAttackSystem.instance.SetSwordCount(0);
                PlayerAttackSystem.instance.SetSpeedCount(0);
                break;
            case "SwordAttack":
                Debug.Log("Sword " + PlayerAttackSystem.instance.totalSword());
                swordItems[PlayerAttackSystem.instance.totalSword()].SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    magicitems[i].SetActive(false);
                    hammerItems[i].SetActive(false);
                    speedFellows[i].SetActive(false);
                }
                PlayerAttackSystem.instance.SetMagicCount(0);
                PlayerAttackSystem.instance.SetHammerCount(0);
                PlayerAttackSystem.instance.SetSpeedCount(0);
                
                Debug.Log("Sword " + PlayerAttackSystem.instance.totalSword());
                break;
            case "GroundHammerAttack":
                hammerItems[PlayerAttackSystem.instance.totalHammer()].SetActive(true);
                for (int i = 0; i < 3; i++)
                {
                    magicitems[i].SetActive(false);
                    swordItems[i].SetActive(false);
                    speedFellows[i].SetActive(false);
                }
                PlayerAttackSystem.instance.SetMagicCount(0);
                PlayerAttackSystem.instance.SetSwordCount(0);
                PlayerAttackSystem.instance.SetSpeedCount(0);
                break;
            case "MageAttack":
                
                break;
            case "Size":
                //todo finding accesories for both size and speed
                break;
            case "Speed":
                speedFellows[PlayerAttackSystem.instance.totalSpeed()].SetActive(true);
                for(int i=0;i<3;i++)
                {
                    swordItems[i].SetActive(false);
                    hammerItems[i].SetActive(false);
                    magicitems[i].SetActive(false);
                }
                PlayerAttackSystem.instance.SetHammerCount(0);
                PlayerAttackSystem.instance.SetSwordCount(0);
                PlayerAttackSystem.instance.SetMagicCount(0);
                break;
        }
    }
}
