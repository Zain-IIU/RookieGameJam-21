using System;
using UnityEngine;

public class PlayerSpeedAttack : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    private GameObject[] enemies;
    
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, ClosestEnemy().position, speed * Time.deltaTime);
        transform.LookAt(ClosestEnemy());
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
