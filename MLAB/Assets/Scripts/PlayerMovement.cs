using System.Collections;
using System.Collections.Generic;
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
        //checking for Drag (mouse /touch)
        /*
        if (Input.GetMouseButtonDown(0)) return;
        
        if (Input.GetMouseButton(0))
        {
            if (!hasClicked)
            {
                //storing first touch pos
                _starttouchPos = Input.mousePosition;
                hasClicked = true;
            }
            if (Input.GetAxis("Mouse X") != 0 )
            {
                direction = Input.mousePosition - _starttouchPos;

                //applying turning relative to direction of Drag
                if (direction.x > 0 && transform.position.x<2 )
                {
                    //turning Right
                  //  transform.Translate(transform.right * turningSpeed * Time.deltaTime);
                    if (transform.rotation.y < 25f)
                        transform.Rotate(0, rotationMultiplier * Time.deltaTime, 0);

                }
                    
                else if (direction.x < 0  && transform.position.x > -2)
                {
                    //turning Left
                  //  transform.Translate(transform.right * -turningSpeed * Time.deltaTime);
                    if (transform.rotation.y > -25f)
                        transform.Rotate(0, -rotationMultiplier * Time.deltaTime, 0);


                }

            }
            
        }

        else
        {
            //restoring touch variables after player lifts the finger
            hasClicked = false;
            _starttouchPos = Vector3.zero;
            direction = Vector3.zero;
            tempRotation = transform.rotation;

            if (transform.rotation.y>0 || transform.rotation.y < 0)
            {  
                tempRotation.y= Mathf.Lerp(tempRotation.y, 0, _timeforNormalDirection);
                transform.rotation = tempRotation;
            }
            
        }
        */
    }
    #endregion


    


}
