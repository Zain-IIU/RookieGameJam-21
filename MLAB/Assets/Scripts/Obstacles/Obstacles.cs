using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject obstacleHitFx;
    private Collider obstacleCol;
    
    public bool isWall;
    
    public GameObject spinningSpikeColGroup;
    
    private void Awake()
    {
        obstacleCol = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetTimer();
        if(other.gameObject.TryGetComponent(out PlayerAttackSystem playerAttackSystem))
        {
            if (spinningSpikeColGroup != null)
            {
                spinningSpikeColGroup.SetActive(false);
            }
            
            obstacleCol.enabled = false;
            
            if (isWall)
            {
                HandleWallBreak(playerAttackSystem);
            }
            else
            {
                PlayerHealthSystem.instance.DamagePlayer(50);
            }
        }
    }

    void HandleWallBreak(PlayerAttackSystem playerAttackSystem)
    {
        obstacleHitFx.SetActive(true);
        if (playerAttackSystem.GetCurrentPower() != PowerType.MuscleAttack)
        {
            PlayerHealthSystem.instance.DamagePlayer(50);
            Destroy(gameObject);
        }
        else
        {
            foreach (Transform t in transform)
            {
                Rigidbody wallRb = t.GetComponent<Rigidbody>();

                if (wallRb != null)
                {
                    wallRb.useGravity = true;
                    wallRb.isKinematic = false;
                            
                    Vector3 dir = t.position - transform.position;
                    dir.Normalize();
                        
                    //  wallRb.AddForce(-dir * 6f, ForceMode.Impulse);
                    wallRb.AddForce(Vector3.forward * 7f, ForceMode.Impulse);
                    wallRb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
                    wallRb.AddForce(new Vector3(Random.Range(-1f, 1f), 0f, 0f) * 3f, ForceMode.Impulse);
                }
            }
        }
    }

    void ResetTimer()
    {
        if (Time.timeScale < 1f)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}   