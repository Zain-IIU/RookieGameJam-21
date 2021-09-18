using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public bool destroyAfterSeconds;
    
    private void Awake()
    {
        if (playerTransform == null)
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (transform.position.z < playerTransform.position.z - 5f)
        {
            if (!destroyAfterSeconds)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 3f);
            }
           
        }

    }
}
