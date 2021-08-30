using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement Variables")]
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float turningSpeed;
    [SerializeField]
    float rotationMultiplier;
    [SerializeField]
    float _timeforNormalDirection;


    Vector3 _starttouchPos;
    Vector3 newDirection;
    Vector3 direction;
    bool hasClicked;
    Quaternion tempRotation;
    #endregion


    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
         hasClicked = false;
        _starttouchPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //for moving straight
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        //checking for Drag (mouse /touch)
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
                    transform.Translate(transform.right * turningSpeed * Time.deltaTime);
                    if (transform.rotation.y < 25f)
                        transform.Rotate(0, rotationMultiplier * Time.deltaTime, 0);

                }
                    
                else if (direction.x < 0  && transform.position.x > -2)
                {
                    //turning Left
                    transform.Translate(transform.right * -turningSpeed * Time.deltaTime);
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


    }
    #endregion


    


}
