using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

using UnityEngine.UI;
public class ElementHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouse_over = false;
    [SerializeField]
    Transform playPanel;
    GameObject Player;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;
        Player.GetComponent<PlayerMovement>().enabled = false;
    }
    void Update()
    {
        if (mouse_over && Input.GetMouseButton(0))
        {

            playPanel.DOScale(Vector2.zero, 0.15f);
            Player.GetComponent<PlayerMovement>().enabled = true;
            Player.GetComponent<Animator>().SetBool("hasStarted", true);
            GetComponent<ElementHandler>().enabled = false;

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
        Debug.Log("Mouse enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
        Debug.Log("Mouse exit");
    }
}
