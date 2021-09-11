using System;
using UnityEngine;


public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private GameObject enemyDeadFX;

    private bool isDestroyed;
  
    private RagDollEnemy ragDollEnemy;

    private void Awake()
    {
        ragDollEnemy = GetComponent<RagDollEnemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Sword")) && ragDollEnemy!=null)
        {
            ragDollEnemy.EnableRagdoll();
        }

        //if (collision.gameObject.CompareTag("Sword"))
        //{
        //    ragDollEnemy.EnableRagdoll();
        //}

        if (collision.gameObject.CompareTag("Player") && collision.transform.localScale != Vector3.one)
        {
            // todo add crushing sound
            Instantiate(enemyDeadFX, transform.position, enemyDeadFX.transform.rotation);
            Destroy(gameObject);
        }
    }


}