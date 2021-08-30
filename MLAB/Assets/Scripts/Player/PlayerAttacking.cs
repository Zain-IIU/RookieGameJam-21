using System.Collections;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    public static PlayerAttacking instance;

    [SerializeField] private Transform raypoint;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask enemyMask;
    
    public string animationTrigger;

    private Animator animator;

    public static bool runOnce;
    
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, rayDistance, enemyMask))
        {
            if (hitInfo.collider != null && !runOnce)
            {
                runOnce = true;
                animator.SetTrigger(animationTrigger);
            }
        }
    }
}
