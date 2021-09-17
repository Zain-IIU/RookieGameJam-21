using UnityEngine;

[System.Serializable]
public class PlayerAccessories 
{
    [Header("Magic Attack")]
    public GameObject[] magicitems;

    [Header("Sword Attack")]
    public GameObject[] swordItems;

    [Header("Hammer Attack")]
    public GameObject[] hammerItems;
    
    [Header("Speed Attack")]
    public GameObject[] speedItems;

    [Header("Size Attack")]
    public GameObject[] sizeItems;

    [Header("Mage Fellow")]
    public GameObject[] mageFellows;

    [Header("Speed Fellow")]
    public GameObject[] speedFellows;
    
    [Header("Hammer Effect")]
    public GameObject[] hammerFX;

    [Header("Muscle Attack")] 
    public GameObject[] muscleItems;

}