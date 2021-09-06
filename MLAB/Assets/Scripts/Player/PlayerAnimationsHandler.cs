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
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Transform groundCheckPoint;

    bool isGrounded;
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
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, 0.4f, groundLayer);
        Anim.SetBool("inAir", !isGrounded);
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
                MageFellowAnimations("MageProjectileAttack");
                break;
            case PowerType.SwordAttack:
                Anim.SetTrigger("SwordNormalAttack");
                break;
            case PowerType.GroundHammerAttack:
                Anim.SetTrigger("HammerProjectileAttack");
                break;
            case PowerType.SpeedAttack:
             
                break;
        }
    }
    public void SetPlayerState(PowerType newState)
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

    public void ResetPlayerSize()
    {
        this.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBounce);
    }

    public void ResetPlayerPowers(PowerType newPower)
    {
        PlayerAccessoriesHolder.instance.ResetAllAcessories();
        if (curPlayerState==PowerType.SizeAttack)
            this.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBounce);
        curPlayerState = newPower;

    }
}
