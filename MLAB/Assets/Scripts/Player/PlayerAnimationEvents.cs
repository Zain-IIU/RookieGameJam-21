using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject groundPunchPowerEffects;
    [SerializeField] private GameObject magicPunchPowerEffects;
    [SerializeField] private GameObject mageGroundStonePowerEffect;
   
    [SerializeField] GameObject magicProjectile;

    [SerializeField] Transform projectilePoints;
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;
    
    [SerializeField] private float forceAmount = 10f;


 
    public void DetectEnemiesInRange()
    {
        groundPunchPowerEffects.SetActive(true);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                collider.attachedRigidbody.AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            }
        }
      
    }
    public void MagicRangeAttack()
    {
        magicPunchPowerEffects.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius*2, enemyLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                collider.attachedRigidbody.AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            }
        }

    }

    public void MagicAttackProjectile()
    {
        GameObject prohjectile = Instantiate(magicProjectile, projectilePoints.transform.position, Quaternion.identity);
    }

    public void MagicGroundPowerAttack()
    {
        mageGroundStonePowerEffect.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
