using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsHandler : MonoBehaviour
{
    public static PlayerAnimationsHandler instance;
    Animator Anim;

    [SerializeField]
    Animator[] mageFellows;
    [SerializeField]
    string curPlayerState;
    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetMoveAnimation(curPlayerState);

    }

    void SetMoveAnimation(string state)
    {
        if(state=="MagicAttack")
        {
            Anim.SetBool("MageRun", true);
        }
        else
        {
            Anim.SetBool("MageRun", false);
        }
    }


    public void SetTransitions(string playerState)
    {
        switch (playerState)
        {
            case "MagicAttack":
                Anim.SetTrigger("MageProjectileAttack");
                MageFellowAnimations("MageProjectileAttack");
                break;
            case "SwordAttack":
                Anim.SetTrigger("SwordNormalAttack");
                break;
            case "GroundHammerAttack":
                Anim.SetTrigger("HammerProjectileAttack");
                break;
            case "MageAttack":

                break;
        }
    }
    public void SetPlayerState(string newState)
    {
        curPlayerState = newState;

    }

    public void MageFellowAnimations(string animTrigger)
    {
        foreach (var mageAnim in mageFellows)
        {
            if (mageAnim.gameObject.activeInHierarchy)
            {
                mageAnim.SetTrigger(animTrigger);
            }
        }
    }
}
