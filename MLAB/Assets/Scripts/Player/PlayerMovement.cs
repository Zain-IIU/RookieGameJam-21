using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement Variables")]
    
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float moveSpeed;
    float yRot;

    #endregion


    public void SetMoveSpeed(float newSpeed) => moveSpeed = newSpeed;

    #region Unity Functions

  
    // Update is called once per frame
    void Update()
    {
        //for moving straight
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));

        if (Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        // handling rotation
        if (Input.GetMouseButton(0))
        {
            yRot += Input.GetAxis("Mouse X") * rotationSpeed;
            yRot = Mathf.Clamp(yRot, -20f, 20f);
            transform.DORotateQuaternion(Quaternion.Euler(0f, yRot, 0f), 0.15f);
        }
        else
        {
            transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.25f);
        }
    }


    #endregion


    


}
