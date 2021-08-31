using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    float speedDecrement;
    [SerializeField]
    float sizeDecrement;

    [SerializeField]
    bool isMoveable;

    private void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PickUpManager.instance.DecrementPower(speedDecrement,sizeDecrement);
        }
    }
}
