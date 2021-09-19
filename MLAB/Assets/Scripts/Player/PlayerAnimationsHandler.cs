using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimationsHandler : MonoBehaviour
{
    public static PlayerAnimationsHandler instance;
    Animator Anim;

    [SerializeField]
    Animator[] mageFellows;
    [SerializeField]
    PowerType curPlayerState;

    [SerializeField] private Transform playerSpine;

    private PlayerAccessoriesHolder playerAccessoriesHolder;
    private PlayerAttackSystem playerAttackSystem;
    private static readonly int MageRun = Animator.StringToHash("MageRun");
    private static readonly int MageProjectileAttack = Animator.StringToHash("MageProjectileAttack");
    private static readonly int SwordNormalAttack = Animator.StringToHash("SwordNormalAttack");
    private static readonly int HammerProjectileAttack = Animator.StringToHash("HammerProjectileAttack");

    private void Awake()
    {
        instance = this;
        playerAccessoriesHolder = GetComponent<PlayerAccessoriesHolder>();
        playerAttackSystem = GetComponent<PlayerAttackSystem>();
        Anim = GetComponent<Animator>();

        if (playerSpine == null)
        {
            playerSpine = GameObject.FindGameObjectWithTag("PlayerSpine").transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        SetMoveAnimation(curPlayerState);
       
    }

    void SetMoveAnimation(PowerType powerType)
    {
        if(powerType == PowerType.MagicAttack)
        {
            Anim.SetBool(MageRun, true);
        }
        else
        {
            Anim.SetBool(MageRun, false);
        }
    }

    public void SetTransitions(PowerType playerState)
    {
        switch (playerState)
        {
            case PowerType.MagicAttack:
                Anim.SetTrigger(MageProjectileAttack);
                MageFellowAnimations("MageProjectileAttack", true);
                break;
            case PowerType.SwordAttack:
                Anim.SetTrigger(SwordNormalAttack);
                break;
            case PowerType.GroundHammerAttack:
                Anim.SetTrigger(HammerProjectileAttack);
                break;
        }
    }
    public void SetPlayerState(PowerType newState)
    {
        curPlayerState = newState;

    }

    public void MageFellowAnimations(string animTrigger, bool isFellow)
    {
        foreach (var mageAnim in mageFellows)
        {
            if (mageAnim.gameObject.activeInHierarchy)
            {
                if (isFellow)
                {
                    mageAnim.SetTrigger(animTrigger);
                }
                else
                {
                    mageAnim.ResetTrigger(animTrigger);
                }
            }
        }
    }


    /*public void ResetTransitions(PowerType playerState)
    {
        switch (playerState)
        {
            case PowerType.MagicAttack:
                Anim.ResetTrigger(MageProjectileAttack);
                MageFellowAnimations("MageProjectileAttack",false);
                break;
            case PowerType.SwordAttack:
                Anim.ResetTrigger(SwordNormalAttack);
                break;
            case PowerType.GroundHammerAttack:
                Anim.ResetTrigger(HammerProjectileAttack);
                break;
        }
    }*/
    
    public void ResetPlayerPowers(PowerType newPower)
    {
        playerAccessoriesHolder.ResetAllAcessories();
        playerAttackSystem.SetCurPower(newPower);
        
        if (curPlayerState==PowerType.SizeAttack)
           transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBounce);
        if (curPlayerState == PowerType.MuscleAttack)
            playerSpine.DOScale(1f, 0.25f).SetEase(Ease.InBounce);
            
        curPlayerState = newPower;
    }
}
