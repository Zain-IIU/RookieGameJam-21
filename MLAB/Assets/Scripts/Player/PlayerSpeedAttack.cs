using System;
using DG.Tweening;
using UnityEngine;

public class PlayerSpeedAttack : MonoBehaviour
{
    [SerializeField] private float maxDistanceRangeToDetectEnemy = 15f;
    private float distanceToEnemy;
    [SerializeField] private float speed = 15f;
    private GameObject[] enemies;
    private GameObject boss;

    private Vector3 startPos;

    private bool isEnemyInRange;

    public bool miniPlayerForBoss;

    private RagDollEnemy ragDollEnemy;
   
    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    private void Start()
    {
        ragDollEnemy = boss.GetComponent<RagDollEnemy>();
    }

    private void OnEnable()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        if(!miniPlayerForBoss){
            if (ClosestEnemy() != null)
            {
                distanceToEnemy = Vector3.Distance(transform.position, ClosestEnemy().position);

                if (distanceToEnemy < maxDistanceRangeToDetectEnemy)
                {
                    transform.position =
                        Vector3.MoveTowards(transform.position, ClosestEnemy().position, speed * Time.deltaTime);
                    transform.LookAt(ClosestEnemy());
                    isEnemyInRange = true;
                }
                else if (isEnemyInRange && (distanceToEnemy > maxDistanceRangeToDetectEnemy))
                {
                    transform.LookAt(startPos);
                    transform.DOLocalMove(startPos, 0.5f).OnComplete(() => transform.DORotate(Vector3.zero, 0.01f));

                    isEnemyInRange = false;
                }
            }
            else if (ClosestEnemy() == null)
            {
                transform.DOLocalMove(startPos, 0.5f).OnComplete(()=>gameObject.SetActive(false));
            }
        }

        if (Vector3.Distance(transform.position, boss.transform.position) < 20f && miniPlayerForBoss)
        {
            transform.DOMove(boss.transform.position, 5f)
                .OnComplete(() => gameObject.SetActive(false));
            transform.LookAt(boss.transform);
        }

    }

    Transform ClosestEnemy()
    {
        Transform targetEnemy = null;
        float closestDistance = Mathf.Infinity;

        Vector3 currentPos = transform.position;
        foreach (GameObject target in enemies)
        {
            if (target != null)
            {
                Vector3 dirToTarget = target.transform.position - currentPos;
                float dist = dirToTarget.magnitude;

                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    targetEnemy = target.transform;
                }
            }
        }

        return targetEnemy;
    }

    private bool hitOnce;

    private void OnTriggerEnter(Collider other)
    {
        if (!hitOnce && other.gameObject.CompareTag("Enemy"))
        {
            hitOnce = true;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        
        if (other.gameObject.CompareTag("Boss"))
        {
            ragDollEnemy.EnableRagdoll();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.isTrigger) return;

        if (!hitOnce && other.gameObject.CompareTag("Enemy"))
        {
            hitOnce = true;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

}
