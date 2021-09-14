using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private Ease mainMenuPanelEase;
    
    [SerializeField] private RectTransform handIconImg;
   
    [SerializeField] private TextMeshProUGUI gemScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    
    [SerializeField] private RectTransform amazingText;
    public RectTransform gemScoreTransform;
    
    [SerializeField] private CanvasGroup settingPanel;
    [SerializeField] private CanvasGroup levelCompletePanel;
    [SerializeField] private CanvasGroup gameOverPanel;
    [SerializeField] private RectTransform settingButtonTransform;

    //for final score tweening
    [SerializeField] private RectTransform targetPos;
    [SerializeField] private RectTransform gemImage;

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
        gemScoreTransform.DOScale(1f, 0.1f).From(0.8f).SetEase(Ease.InFlash);
    }

    public void SetFinalScore(string newText)
    {
        finalScoreText.text = newText;
        
    }

    public void OnSettingButtonPressed()
    {
        settingButtonTransform.DOScale(1.5f, 0.25f).SetEase(mainMenuPanelEase);
        PanelsTweenerEffect(mainMenuPanel,true);
        PanelsTweenerEffect(settingPanel);
    }

    public void InGameTextTweener()
    {
        amazingText.gameObject.SetActive(true);
        amazingText.DOAnchorPos(new Vector2(0, 580f), 0.75f);
        amazingText.GetComponent<Image>().DOFade(0, 0.75f).OnComplete(() =>
        {
            amazingText.DOAnchorPos(Vector2.zero, 3f);
            amazingText.gameObject.SetActive(false);
        });
    }
    public void OnCloseSettingButtonPressed()
    {
        PanelsTweenerEffect(settingPanel,true);
        PanelsTweenerEffect(mainMenuPanel);
        settingButtonTransform.DOScale(1f, 0.25f).SetEase(mainMenuPanelEase);
    }

    public void OnLevelComplete()
    {
        PanelsTweenerEffect(levelCompletePanel);
        gemImage.gameObject.SetActive(true);
        gemImage.DORotate(targetPos.transform.localEulerAngles, 0.75f);
        gemImage.DOMove(targetPos.position, 1.5f).OnComplete(() =>
        {
            SetFinalScore(ScoreManager.instance.GetCurrentScore().ToString());
            gemImage.gameObject.SetActive(false);
        });
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
            canvasGroup.DOFade(1f, 0.35f).From(0f);
            canvasGroup.transform.DOScale(Vector3.one, 0.3f).From(new Vector3(0.75f, 0.35f)).SetEase(mainMenuPanelEase);
        }
        else
        {
            canvasGroup.DOFade(0f, 0.35f).From(1f).OnComplete(()=>canvasGroup.gameObject.SetActive(false));
        }
    }

    void ScaleTweenerEffect(RectTransform rectTransform, float endVal, float duration)
    {
        rectTransform.DOScale(endVal, duration).SetEase(mainMenuPanelEase);
        
    }
}
