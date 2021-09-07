using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private Transform firePoint;
    
    void Shoot()
    {
        Instantiate(enemyBullet, firePoint.position, Quaternion.identity);
    }
}
