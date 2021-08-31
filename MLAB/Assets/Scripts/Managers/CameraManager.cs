using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    
    [SerializeField] CinemachineVirtualCamera followCam;
    [SerializeField] CinemachineVirtualCamera actionCam_01;
    [SerializeField] CinemachineVirtualCamera actionCam_02;

    private void Awake()
    {
        instance = this;
    }

    public void ToggleFollowCam(bool isActive)
    {
        followCam.gameObject.SetActive(isActive);
    }

    public void ToggleActionCam_01(bool isActive)
    {
        actionCam_01.gameObject.SetActive(isActive);
    }
    
    public void ToggleActionCam_02(bool isActive)
    {
        actionCam_02.gameObject.SetActive(isActive);
    }


    
}
