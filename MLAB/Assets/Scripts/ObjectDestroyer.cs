using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (transform.position.z < playerTransform.position.z - 5f)
        {
            Destroy(gameObject);
        }

    }
}
