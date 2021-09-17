using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject obstacleHitFx;
    
    public bool isWall;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerAttackSystem playerAttackSystem))
        {
            if (isWall)
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
                            
                            Vector3 dir = t.position - transform.position;
                            dir.Normalize();
                        
                            wallRb.AddForce(dir * 5f, ForceMode.Impulse);
                            wallRb.AddForce(Vector3.forward * 2f, ForceMode.Impulse);
                        }
                      
                    }
                }
            }
            else
            {
                PlayerHealthSystem.instance.DamagePlayer(50);
            }
        }
    }
}   