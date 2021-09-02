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
            transform.position = new Vector3(0f, 0f, target.position.z);
        }
        else
        {
            transform.position = target.position;
        }
      
    }
}
