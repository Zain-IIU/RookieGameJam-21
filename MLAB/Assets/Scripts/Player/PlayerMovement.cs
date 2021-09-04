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

    public static bool isPerformingAttack;
    
    #endregion


    public void SetMoveSpeed(float newSpeed) => moveSpeed = newSpeed;
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    #region Unity Functions

  
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameStarted || GameManager.instance.isGameOver) return;
        if (isPerformingAttack) return;
        
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

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), transform.position.y,
            transform.position.z);
    }


    #endregion


    


}
