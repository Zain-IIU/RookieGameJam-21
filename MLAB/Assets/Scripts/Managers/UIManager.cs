using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    RectTransform settingsPanel;
    [SerializeField]
    Transform shopPanel;
    [SerializeField]
    Transform newPanel;
    [SerializeField]
    Transform playPanel;
    [SerializeField]
    Transform playButton;
    [SerializeField]
    Ease easetye;
    [SerializeField]
    float easeTimer;


    bool hasPressed;
    bool hasPressedShop;
    bool hasPressedPanel;
    bool hasPressedPlay;
    int childCount;
    // Start is called before the first frame update
    void Start()
    {
        hasPressed = false;
        hasPressedShop = false;
        hasPressedPanel = false;
        hasPressedPlay = false;
        childCount = settingsPanel.transform.childCount - 1;
        GameManager.instance.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        // todo : Later
        playPanel.gameObject.SetActive(false);
    }


    public void TweenSettingsPanel()
    {
        if (!hasPressed)
        {
            for (int i = 0; i <= childCount; i++)
            {
                settingsPanel.transform.GetChild(i).gameObject.SetActive(true);
                settingsPanel.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosY(-50f * (i + 1), easeTimer).SetEase(easetye);
            }
            hasPressed = true;
        }

        else
        {
            for (int i = 0; i <= childCount; i++)
            {
                settingsPanel.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPosY(0, easeTimer).SetEase(easetye);
                settingsPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
            hasPressed = false;
        }

    }
   

    public void TweenShopPanel()
    {
        if (!hasPressedShop)
        {
            shopPanel.DOScale(Vector3.one, 0.15f).SetEase(easetye);
            newPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedShop = true;
            hasPressedPanel = false;
        }
        else
        {
            shopPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedShop = false;
        }
    }

    public void TweenNewPanel()
    {
        if (!hasPressedPanel)
        {
            newPanel.DOScale(Vector3.one, 0.15f).SetEase(easetye);
            shopPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedPanel = true;
            hasPressedShop = false;
        }
        else
        {
            newPanel.DOScale(Vector3.zero, 0.15f).SetEase(easetye);
            hasPressedPanel = false;
        }

    }

}
