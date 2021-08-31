using UnityEngine;

public class ProjectileInitForce : MonoBehaviour
{
    private Rigidbody rigidbody;

    [SerializeField] private float forceAmount = 30f;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigidbody.AddForce(transform.forward * forceAmount, ForceMode.Impulse);
    }

}
