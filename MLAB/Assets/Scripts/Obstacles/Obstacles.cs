using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    PowerType power;
    
    private static int numberOfFalls;

    private void OnTriggerEnter(Collider other)
    {
        bool playerTag = other.gameObject.CompareTag("Player");
        
        if(playerTag)
        {
            /*numberOfFalls++;
            Debug.Log(numberOfFalls);
            if (numberOfFalls < 2)
            {
                PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
                other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
                other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
               
                
            }
            //todo game over logic
            else
            {
                PlayerAnimationsHandler.instance.ResetPlayerPowers(power);
                other.gameObject.GetComponent<Animator>().SetTrigger("ObstacleSize");
                other.gameObject.GetComponent<PlayerMovement>().SetMoveSpeed(0f);
                GameManager.instance.isGameOver = true;
                UIManager.instance.OnGameOver();
            }*/
        }
        

    }


}   