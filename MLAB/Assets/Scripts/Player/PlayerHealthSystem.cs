using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
      public static PlayerHealthSystem instance;

      private PlayerMovement playerMovement;
      private PlayerAnimationsHandler playerAnimationsHandler;
      private Collider mainCollider;
      
        [SerializeField]
        int totalHealth;
    
        int curHealth;
    
    
        Animator Anim;
        private static readonly int ObstacleSize = Animator.StringToHash("ObstacleSize");

        private void Awake()
        {
            instance = this;
            playerMovement = GetComponent<PlayerMovement>();
            playerAnimationsHandler = GetComponent<PlayerAnimationsHandler>();
            mainCollider = GetComponent<Collider>();

        }
        private void Start()
        {
            curHealth = totalHealth;
            Anim = GetComponent<Animator>();
        }
        public void DamagePlayer(int amount)
        {
            curHealth -= amount;
    
            if (curHealth <= 50 && curHealth>0)
            { 
                PlayerAnimationsHandler.instance.ResetPlayerPowers(PowerType.Null);
               Anim.SetTrigger(ObstacleSize);
               playerMovement.SetMoveSpeed(0f);
            }
            else
            {
                GameManager.instance.OnGameOver();
                playerAnimationsHandler.ResetPlayerPowers(PowerType.Null);
                Anim.SetTrigger("ObstacleSize");
                Anim.SetBool("isGameOver", GameManager.instance.isGameOver);
                playerMovement.SetMoveSpeed(0f);
                mainCollider.attachedRigidbody.useGravity = false;
                mainCollider.enabled = false;
            }
        }
   
}

