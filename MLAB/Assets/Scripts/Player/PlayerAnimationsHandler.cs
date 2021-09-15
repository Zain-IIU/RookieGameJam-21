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

    void SetMoveAnimation(PowerType powerType)
    {
        if(powerType == PowerType.MagicAttack)
        {
            Anim.SetBool("MageRun", true);
        }
        else
        {
            Anim.SetBool("MageRun", false);
        }
    }

    public void SetTransitions(PowerType playerState)
    {
        switch (playerState)
        {
            case PowerType.MagicAttack:
                Anim.SetTrigger("MageProjectileAttack");
                MageFellowAnimations("MageProjectileAttack", true);
                break;
            case PowerType.SwordAttack:
                Anim.SetTrigger("SwordNormalAttack");
                break;
            case PowerType.GroundHammerAttack:
                Anim.SetTrigger("HammerProjectileAttack");
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

    public void ResetPlayerSize()
    {
         transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBounce);
    }

    public void ResetTransitions(PowerType playerState)
    {
        switch (playerState)
        {
            case PowerType.MagicAttack:
                Anim.ResetTrigger("MageProjectileAttack");
                MageFellowAnimations("MageProjectileAttack",false);
                break;
            case PowerType.SwordAttack:
                Anim.ResetTrigger("SwordNormalAttack");
                break;
            case PowerType.GroundHammerAttack:
                Anim.ResetTrigger("HammerProjectileAttack");
                break;
        }
    }
    
    public void ResetPlayerPowers(PowerType newPower)
    {
        PlayerAccessoriesHolder.instance.ResetAllAcessories();
        PlayerAttackSystem.instance.SetCurPower(newPower);
        if (curPlayerState==PowerType.SizeAttack)
           transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBounce);
        curPlayerState = newPower;

    }
}
