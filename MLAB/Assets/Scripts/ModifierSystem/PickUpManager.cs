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
            case "Speed":
                speedVal = playerMovement.GetMoveSpeed();
                speedVal -= decrementSpeed;
                speedVal = Mathf.Clamp(speedVal, 10f, 15f);
                playerMovement.SetMoveSpeed(speedVal);
                break;
            case "Size":

                /*CurSize = transform.localScale;
                CurSize.x -= decrementSize;
                CurSize.y -= decrementSize;
                CurSize.z -= decrementSize;
                Debug.Log(CurSize);
                CurSize.x = Mathf.Clamp(CurSize.x, 1f, 2f);
                CurSize.y = Mathf.Clamp(CurSize.x, 1f, 2f);
                CurSize.z = Mathf.Clamp(CurSize.x, 1f, 2f);*/
                transform.DOScale(Vector3.one, 0.5f);
                
                break;
            default:
                break;
        }
    }
    
}
