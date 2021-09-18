using System;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private GameObject enemyDeadFX;
    [SerializeField] private GameObject hitDeadFX;

    private RagDollEnemy ragDollEnemy;

    private void Awake()
    {
        ragDollEnemy = GetComponent<RagDollEnemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && ragDollEnemy!=null)
        {
           
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);

            foreach (Collider collider in colliders)
            {
                if (collider.GetComponent<RagDollEnemy>() != null)
                {
                    collider.GetComponent<RagDollEnemy>().EnableRagdoll();
                }
            }

            Instantiate(hitDeadFX, collision.contacts[0].point, hitDeadFX.transform.rotation, transform);
        }

        if (collision.gameObject.CompareTag("Player") && collision.transform.localScale != Vector3.one)
        {
            // todo add crushing sound
            Instantiate(enemyDeadFX, transform.position, enemyDeadFX.transform.rotation);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") &&
            PlayerAttackSystem.instance.GetCurrentPower() == PowerType.MuscleAttack)
        {
            ragDollEnemy.EnableRagdoll();
            Instantiate(hitDeadFX, transform.position, hitDeadFX.transform.rotation, transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Sword") && ragDollEnemy != null)
        {
            ragDollEnemy.EnableRagdoll();
            Instantiate(hitDeadFX, transform.position, hitDeadFX.transform.rotation, transform);
        }


    }
}