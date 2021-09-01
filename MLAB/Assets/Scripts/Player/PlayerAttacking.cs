using System;
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

    public bool hasConsumed;
    
    private void Awake()
    {
        instance = this;
        hasConsumed = false;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
       animator.SetBool("hasStarted", true);
    }

    private void Update()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(raypoint.position, raypoint.forward, out hitInfo, rayDistance, enemyMask))
        {
            if (hitInfo.collider != null && !runOnce && hasConsumed)
            {
                runOnce = true;
                hasConsumed = false;
                animator.SetTrigger(animationTrigger);
               
            }
        } 
    }

    private void OnCollisionEnter(Collision other)
    {
        if (transform.localScale != Vector3.one && other.gameObject.CompareTag("Enemy"))
        {
            other.collider.attachedRigidbody.AddForce(Vector3.forward * 30 + Vector3.up * 16f, ForceMode.Impulse);
        }
    }
}
