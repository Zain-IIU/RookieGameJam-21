using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement Variables")]
    
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float moveSpeed;
    
    [SerializeField] GameObject climbPoint;
    [SerializeField] GameObject runPoint;
    [SerializeField] private Transform targetX10Point;
    
    private Animator animator;
    private GameManager gameManager;
    
    float xRot;
    float yRot;
    
    bool isClimbing;

    public static bool isPerformingAttack;
    Rigidbody RB;

    public LayerMask groundLayer;
    public float distToGround = 2f;
    private bool isLanded;
    
    
    #endregion

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        gameManager = GameManager.instance;
        
        isLanded = true;
    }
    public void SetMoveSpeed(float newSpeed) => moveSpeed = newSpeed;
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    #region Unity Functions

  
    // Update is called once per frame
    void Update()
    {
      
        if (!gameManager.isGameStarted || gameManager.isGameOver || gameManager.isLevelCompleted) return;
      //  if (isPerformingAttack) return;
        
        //for moving straight
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime),Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            return;
        }
        
        // handling rotation
        
            if (Input.GetMouseButton(0) && !isPerformingAttack)
            {
            if (!isClimbing)
            {
                yRot += Input.GetAxis("Mouse X") * rotationSpeed;
                yRot = Mathf.Clamp(yRot, -20f, 20f);
                transform.DORotateQuaternion(Quaternion.Euler(0f, yRot, 0f), 0.15f);
            }

            else
            {
                xRot -= Input.GetAxis("Mouse X") * rotationSpeed;
                xRot = Mathf.Clamp(xRot, -115f, -65f);
                transform.DORotateQuaternion(Quaternion.Euler(xRot, -90, 90f), 0.15f);
            }
        }
        else
        {
            if(!isClimbing)
                transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.25f);
            else
                transform.DORotateQuaternion(Quaternion.Euler(-90f, -90f, 90f), 0.25f);
        }

        
        if (Physics.Raycast(transform.position, Vector3.down, distToGround, groundLayer) && !isLanded)
        {
            isLanded = true;
            animator.SetTrigger("Land");
            GameManager.instance.LevelCompleted();
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), transform.position.y,
            transform.position.z);
    }

    
    #endregion

    private bool reachedPeakPoint;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject==climbPoint && !reachedPeakPoint)
        {
            isClimbing = true;
            RB.useGravity = false;
            xRot = -90f;
            CameraManager.instance.PrioritizeWallCam(10, 15);
            
            RB.constraints = RigidbodyConstraints.None;
            RB.freezeRotation = true;
            RB.velocity = Vector3.zero;
        }
        if(collision.gameObject == runPoint)
        {
            isClimbing = false;
            RB.useGravity = true;
            xRot = 0f;
            CameraManager.instance.PrioritizeWallCam(15, 10);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            xRot = 0f;
            isLanded = false;
            isClimbing = false;
            animator.SetTrigger("Flip");
     
            RB.AddForce(Vector3.back * 10f, ForceMode.Impulse);
            RB.useGravity = true;
            
            SetMoveSpeed(0f);
            CameraManager.instance.PrioritizeWallCam(15, 10);
        }
        
        if(other.gameObject.CompareTag("Multiplier"))
        {
            ScoreManager.instance.SetMultiliedScore();
            if (ScoreManager.instance.GetMultipliedScore() > 9)
            {
                xRot = 0f;
                reachedPeakPoint = true;
                isClimbing = false;
                transform.parent = targetX10Point;
                transform.DORotateQuaternion(Quaternion.Euler(0f, 0f, 0f), 0.25f);
                transform.localPosition = Vector3.zero;
                SetMoveSpeed(0f);
                animator.SetTrigger("LevelEnd");
                GameManager.instance.LevelCompleted();
            }
           
        }
    }
}
