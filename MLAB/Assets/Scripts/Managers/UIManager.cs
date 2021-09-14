using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private Ease mainMenuPanelEase;
    
    [SerializeField] private RectTransform handIconImg;
   
    [SerializeField] private TextMeshProUGUI gemScoreText;
    public RectTransform gemScoreTransform;
    
    [SerializeField] private CanvasGroup settingPanel;
    [SerializeField] private RectTransform settingButtonTransform;
   
    private GameManager gameManagerInstance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManagerInstance = GameManager.instance;

        PanelsTweenerEffect(mainMenuPanel);
        handIconImg.DOLocalMoveX(220f, 1f).SetLoops(-1, LoopType.Yoyo);
        
        gameManagerInstance.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        PanelsTweenerEffect(mainMenuPanel, true);
    }

    public void SetGemScore(string newText)
    {
        gemScoreText.text = newText;
        gemScoreTransform.DOScale(1f, 0.1f).From(0.8f).SetEase(Ease.InFlash);
    }

    public void OnSettingButtonPressed()
    {
        settingButtonTransform.DOScale(1.5f, 0.25f).SetEase(mainMenuPanelEase);
        PanelsTweenerEffect(mainMenuPanel,true);
        PanelsTweenerEffect(settingPanel);
    }

    public void OnCloseSettingButtonPressed()
    {
        PanelsTweenerEffect(settingPanel,true);
        PanelsTweenerEffect(mainMenuPanel);
        settingButtonTransform.DOScale(1f, 0.25f).SetEase(mainMenuPanelEase);
    }

    void PanelsTweenerEffect(CanvasGroup canvasGroup, bool isOn = false)
    {
        if (!isOn)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.75f).From(0f);
            canvasGroup.transform.DOScale(Vector3.one, 0.5f).From(new Vector3(0.75f, 0.75f)).SetEase(mainMenuPanelEase);
        }
        else
        {
            canvasGroup.DOFade(0f, 0.75f).From(1f).OnComplete(()=>canvasGroup.gameObject.SetActive(false));
        }
    }

    void ScaleTweenerEffect(RectTransform rectTransform, float endVal, float duration)
    {
        rectTransform.DOScale(endVal, duration).SetEase(mainMenuPanelEase);
        
    }
}
