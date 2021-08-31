using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class CameraSplash : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera followCam;
   
    public void ActionCamEnable()
    {
        followCam.Priority = 10;
    }
    public void FollowCamEnable()
    {
        followCam.Priority = 15;
    }
}
