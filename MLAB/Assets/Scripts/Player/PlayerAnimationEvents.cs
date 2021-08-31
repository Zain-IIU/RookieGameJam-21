using System;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject groundPunchPowerEffects;
    [SerializeField] private GameObject magicPunchPowerEffects;

    [SerializeField] GameObject magicProjectile;

    [SerializeField] Transform magicPoint;
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;

    
    [SerializeField] private float forceAmount = 10f;
    
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void AnimationEventSpeed(float newSpeed)
    {
        playerMovement.SetMoveSpeed(newSpeed);
    }

    public void DetectEnemiesInRange()
    {
        groundPunchPowerEffects.SetActive(true);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                Debug.Log(collider.gameObject.name);
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
                Debug.Log(collider.gameObject.name);
                collider.attachedRigidbody.AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
            }
        }

    }

    public void MagicAttackProjectile()
    {
        GameObject prohjectile = Instantiate(magicProjectile, magicPoint.transform.position, Quaternion.identity);
        prohjectile.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 50f);
    }
    public void EnableAnimation()
    {
        PlayerAttacking.runOnce = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
