using System;
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

    private float time = 0;
    public float amplitude = 2;
    public float occilation = 0.5f;
  
    private void Update()
    {
        time += Time.deltaTime;
        
        if (isMoveable)
        {
            Vector3 moveX = transform.localPosition;
            float x = amplitude * Mathf.Sin(time * occilation);
            moveX.x = x;

            transform.localPosition = moveX;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PickUpManager.instance.DecrementPower(speedDecrement,sizeDecrement);
        }
    }
}
