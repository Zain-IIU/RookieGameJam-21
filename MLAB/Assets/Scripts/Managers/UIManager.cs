using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private Ease mainMenuPanelEase;
    
    [SerializeField] private RectTransform handIconImg;
   
    [SerializeField] private TextMeshProUGUI gemScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    
    [SerializeField] private RectTransform[] feedBackText;
    public RectTransform gemScoreTransform;
    
    [SerializeField] private CanvasGroup settingPanel;
    [SerializeField] private CanvasGroup levelCompletePanel;
    [SerializeField] private CanvasGroup gameOverPanel;
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
        if (settingPanel.gameObject.activeSelf)
            PanelsTweenerEffect(settingPanel,true);
        
    }
    
    public void SetGemScore(string newText)
    {
        gemScoreText.text = newText;
        ScaleTweenerEffect(gemScoreTransform, 1f, 0.1f, 0.8f, Ease.InFlash);
    }

    public void SetFinalScore(string newText)
    {
        finalScoreText.text = newText;
        
    }

    public void OnSettingButtonPressed()
    {
        ScaleTweenerEffect(settingButtonTransform, 1.5f, 0.25f, 1f, mainMenuPanelEase);
        PanelsTweenerEffect(mainMenuPanel,true);
        PanelsTweenerEffect(settingPanel);
    }

    public void InGameTextTweener()
    {
        int index = Random.Range(0, feedBackText.Length);
        
        Debug.Log(index);
        feedBackText[index].gameObject.SetActive(true);
        feedBackText[index].DOAnchorPos(new Vector2(0, 500f), 1f).SetEase(mainMenuPanelEase).OnComplete(() =>
        {
            feedBackText[index].DOAnchorPos(new Vector2(2000f, 500f), 2f).SetEase(mainMenuPanelEase);
            feedBackText[index].DOAnchorPos(new Vector2(-2000f, 500f), 0).SetDelay(2f);
        });
    }
    public void OnCloseSettingButtonPressed()
    {
        PanelsTweenerEffect(settingPanel,true);
        PanelsTweenerEffect(mainMenuPanel);
        ScaleTweenerEffect(settingButtonTransform, 1f, 0.25f, 1.5f, mainMenuPanelEase);
    }

    public void OnLevelComplete()
    {
        PanelsTweenerEffect(levelCompletePanel);
        /*gemImage.gameObject.SetActive(true);
        gemImage.DORotate(targetPos.transform.localEulerAngles, 0.75f);
        gemImage.DOMove(targetPos.position, 1.5f).OnComplete(() =>
        {
            SetFinalScore(ScoreManager.instance.GetCurrentScore().ToString());
            gemImage.gameObject.SetActive(false);
        });*/
    }
    public void OnGameOver()
    {
        PanelsTweenerEffect(gameOverPanel);
    }
    void PanelsTweenerEffect(CanvasGroup canvasGroup, bool isOn = false)
    {
        if (!isOn)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.75f).From(0f);
            canvasGroup.transform.DOScale(Vector3.one, 0.75f).From(new Vector3(0.75f, 0.35f)).SetEase(mainMenuPanelEase);
        }
        else
        {
            canvasGroup.DOFade(0f, 0.75f).From(1f).OnComplete(()=>canvasGroup.gameObject.SetActive(false));
        }
    }

    void ScaleTweenerEffect(RectTransform rectTransform, float endVal, float duration, float fromVal, Ease ease)
    {
        rectTransform.DOScale(endVal, duration).SetEase(ease);
        
    }
}
