using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    public bool isMageFollow;
    
    private void LateUpdate()
    {
        if (isMageFollow)
        {
            transform.position = new Vector3(0f,target.position.y, target.position.z);
            transform.localRotation = Quaternion.Euler(target.rotation.x, 0, 0);
        }
        else
        {
            transform.position = target.position;
        }
      
    }
}
