using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private GameObject groundPunchPowerEffects;
    [SerializeField] private GameObject magicPunchPowerEffects;
    [SerializeField] private GameObject spikeGroundAttack;

    [SerializeField] GameObject magicProjectile;
    [SerializeField] GameObject hammerProjectile;

    [SerializeField] Transform projectilePoints;
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private float forceAmount = 10f;

    [SerializeField] private float jumpForce;

    Rigidbody RB;

    [SerializeField] private Transform bossTransform;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void DetectEnemiesInRange()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, enemyLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<RagDollEnemy>() != null)
            {
                collider.GetComponent<RagDollEnemy>().EnableRagdoll();
            }
        }

        UIManager.instance.InGameTextTweener();
    }

    public void JumpAttack()
    {
        RB.AddForce(Vector3.forward * jumpForce / 2 + Vector3.up * jumpForce / 2, ForceMode.Impulse);
    }

    public void MagicRangeAttack()
    {
        magicPunchPowerEffects.SetActive(true);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius * 2, enemyLayerMask);

        foreach (Collider collider in colliders)
        {
            if (collider != null)
            {
                collider.attachedRigidbody.AddForce(Vector3.forward * forceAmount + Vector3.up * forceAmount,
                    ForceMode.Impulse);
            }
        }

    }

    public void MagicAttackProjectile()
    {
        Instantiate(magicProjectile, projectilePoints.transform.position, Quaternion.identity);
    }

    public void SpeedPowerAttack()
    {
        spikeGroundAttack.SetActive(true);
    }

    void HammerThrowAttack()
    {
        Instantiate(hammerProjectile, projectilePoints.transform.position, Quaternion.identity);
    }

    void EnableGroundLighting(GameObject powerAttackEffect)
    {
        groundPunchPowerEffects.SetActive(true);

        Transform lightingTrial =
            Instantiate(powerAttackEffect, transform.position + new Vector3(0, 1.2f, 0f), Quaternion.identity)
                .transform;
        lightingTrial.DOMove(bossTransform.position, 2f);
    }
}
