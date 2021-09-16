using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
      public static PlayerHealthSystem instance;
    
        [SerializeField]
        int totalHealth;
    
        int curHealth;
    
    
        Animator Anim;
    
        private void Awake()
        {
            instance = this;
    
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
               Anim.SetTrigger("ObstacleSize");
               GetComponent<PlayerMovement>().SetMoveSpeed(0f);
            }
            else
            {
                GameManager.instance.OnGameOver();
                PlayerAnimationsHandler.instance.ResetPlayerPowers(PowerType.Null);
                Anim.SetTrigger("ObstacleSize");
                Anim.SetBool("isGameOver", GameManager.instance.isGameOver);
                GetComponent<PlayerMovement>().SetMoveSpeed(0f);
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Collider>().enabled = false;
            }
        }
   
}

