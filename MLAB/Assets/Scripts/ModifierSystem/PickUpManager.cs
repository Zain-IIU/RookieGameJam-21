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
 
    private void Awake()
    {
       
        instance = this;
    }

    public void setPower(string newPower)
    {
        CurPower = newPower;
    }

    public void DecrementPower(float decrementSpeed,float decrementSize)
    {
        switch(CurPower)
        {
            case "Speed":
                speedVal = GetComponent<PlayerMovement>().GetMoveSpeed();
                speedVal -= decrementSpeed;
                speedVal = Mathf.Clamp(speedVal, 10f, 15f);
                GetComponent<PlayerMovement>().SetMoveSpeed(speedVal);
                break;
            case "Size":

                CurSize = transform.localScale;
                CurSize.x -= decrementSize;
                CurSize.y -= decrementSize;
                CurSize.z -= decrementSize;
                Debug.Log(CurSize);
                CurSize.x = Mathf.Clamp(CurSize.x, 1f, 2f);
                CurSize.y = Mathf.Clamp(CurSize.x, 1f, 2f);
                CurSize.z = Mathf.Clamp(CurSize.x, 1f, 2f);
                transform.DOScale(CurSize, 0.25f);
                
                break;
            default:
                break;
        }
    }
    
}
