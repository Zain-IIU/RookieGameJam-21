using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject groundPunchPowerEffects;
    [SerializeField] private GameObject magicPunchPowerEffects;
    [SerializeField] private GameObject speedEnableJuniorPlayers;

    [SerializeField] GameObject magicProjectile;
    [SerializeField] GameObject hammerProjectile;

    [SerializeField] Transform projectilePoints;
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;
    
    [SerializeField] private float forceAmount = 10f;

    [SerializeField] private float jumpForce;

    Rigidbody RB;


    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }
    public void DetectEnemiesInRange()
    {
        groundPunchPowerEffects.SetActive(true);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {

                collider.attachedRigidbody.AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);

                if (collider.GetComponent<RagDollEnemy>() != null)
                {
                    collider.GetComponent<Animator>().enabled = false;
                    Debug.Log("Detected");
                    collider.enabled = false;
                    collider.GetComponent<RagDollEnemy>().ResetRigidBody(false);
                    collider.GetComponent<RagDollEnemy>().ResetColliders(true);

                    collider.GetComponent<Rigidbody>().isKinematic = false;

                    collider.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount, ForceMode.Impulse);
                }
               
            }
        }
      
    }
    public void JumpAttack()
    {
        RB.AddForce(Vector3.forward * jumpForce/2 + Vector3.up * jumpForce,ForceMode.Impulse);
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
       Instantiate(magicProjectile, projectilePoints.transform.position, Quaternion.identity);
    }

    public void SpeedPowerAttack()
    {
        speedEnableJuniorPlayers.SetActive(true);
    }

    void HammerThrowAttack()
    {
        Instantiate(hammerProjectile, projectilePoints.transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
