using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
  public int mutliplierAmount;
  
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Debug.Log("Player entered: " + mutliplierAmount);
    }
  }
}
