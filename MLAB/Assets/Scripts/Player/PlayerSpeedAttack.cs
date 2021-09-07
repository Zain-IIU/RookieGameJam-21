using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class PlayerSpeedAttack : MonoBehaviour
{
    [SerializeField] private float maxDistanceRangeToDetectEnemy = 15f;
    private float distanceToEnemy;
    [SerializeField] private float speed = 15f;
    private GameObject[] enemies;

    private Vector3 startPos;

    private bool isEnemyInRange;

   
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        if (ClosestEnemy() != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, ClosestEnemy().position);

            if (distanceToEnemy < maxDistanceRangeToDetectEnemy)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, ClosestEnemy().position, speed * Time.deltaTime);
                transform.LookAt(ClosestEnemy());
                isEnemyInRange = true;
            }
            else if(isEnemyInRange && (distanceToEnemy > maxDistanceRangeToDetectEnemy))
            {
                transform.LookAt(startPos);
                transform.DOLocalMove(startPos, 1.5f).
                    OnComplete(() => transform.DORotate(Vector3.zero, 0.01f));
            
                isEnemyInRange = false;
            }
        }
        else
        {
          //  gameObject.SetActive(false);
        }
  
    }

    Transform ClosestEnemy()
    {
        Transform targetEnemy = null;
        float closestDistance = Mathf.Infinity;

        Vector3 currentPos = transform.position;
        foreach (GameObject target in enemies)
        {
            if (target != null)
            {
                Vector3 dirToTarget = target.transform.position - currentPos;
                float dist = dirToTarget.magnitude;

                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    targetEnemy = target.transform;
                }
            }
        }

        return targetEnemy;
    }

    private bool hitOnce;
    private void OnCollisionEnter(Collision other)
    {
        if (!hitOnce && other.gameObject.CompareTag("Enemy"))
        {
            hitOnce = true;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
