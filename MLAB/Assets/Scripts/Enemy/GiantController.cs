using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantController : MonoBehaviour
{
    Animator Anim;
    [SerializeField]
    int ID;
    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnPlayerEnter += ActivateAnimator;
    }
    private void Update()
    {
        if (GameManager.instance.isGameOver) Anim.SetBool("playerDead", true);
    }
    void ActivateAnimator(int id)
    {
        if(id == ID && !GameManager.instance.isGameOver)
         Anim.enabled = true;
    }
}
