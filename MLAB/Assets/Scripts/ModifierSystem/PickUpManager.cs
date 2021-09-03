using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class PickUpManager : MonoBehaviour
{
    public static PickUpManager instance;
    
    public string CurPower;


    public static float speedVal;
   

    public static Vector3 CurSize;

    private PlayerMovement playerMovement;
    
    private void Awake()
    {
        instance = this;
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void SetPower(string newPower)
    {
        CurPower = newPower;
    }

    public void DecrementPower(float decrementSpeed,float decrementSize)
    {
        switch(CurPower)
        {
            case "SpeedAttack":
                speedVal = playerMovement.GetMoveSpeed();
                speedVal -= decrementSpeed;
                speedVal = Mathf.Clamp(speedVal, 10f, 15f);
                playerMovement.SetMoveSpeed(speedVal);
                break;
            case "Size":
               
                transform.DOScale(Vector3.one, 0.5f);
                break;
            default:
                break;
        }
    }
    
}
