using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    #region variables
    
    public static UIManager instance;
    
    [SerializeField] private CanvasGroup mainMenuPanel;
    [SerializeField] private Ease mainMenuPanelEase;
    
    [SerializeField] private RectTransform handIconImg;
   
    [SerializeField] private TextMeshProUGUI gemScoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private Ease finalScoreTextEase;

    
    [SerializeField] private RectTransform[] feedBackText;
    public RectTransform gemScoreTransform;
    
    [SerializeField] private CanvasGroup settingPanel;
    [SerializeField] private RectTransform settingButtonTransform;

    [SerializeField] private CanvasGroup gameOverPanel;
   
    [SerializeField] private CanvasGroup levelCompletePanel;
    [SerializeField] private CanvasGroup victoryBannerCanvasGroup;
    [SerializeField] private RectTransform radialLevelCompleteBG;
    [SerializeField] private RectTransform nextButtonTransform;
    private Image nextButtonImg;
    [SerializeField] private Ease nextButtonEase;
    
    [SerializeField] private RectTransform levelCompleteGemImg;
    [SerializeField] private RectTransform gemInitialPos;
    [SerializeField] private RectTransform gemTargetPos;
    [SerializeField] private RectTransform gemHolder;

    [SerializeField] private Image powerSlider;
    [SerializeField] private GameObject maxPowerIndicator;
    
    private GameManager gameManagerInstance;
    
    #endregion
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameManagerInstance = GameManager.instance;

        PanelsTweenerEffect(mainMenuPanel);
        handIconImg.DOLocalMoveX(220f, 1f).SetLoops(-1, LoopType.Yoyo);
        
        nextButtonTransform.gameObject.SetActive(false);
        nextButtonImg = nextButtonTransform.GetComponent<Image>();
        
        gameManagerInstance.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        PanelsTweenerEffect(mainMenuPanel, true);
        if (settingPanel.gameObject.activeSelf)
            PanelsTweenerEffect(settingPanel,true);
        
    }
    
    public void SetGemScoreText(string newText)
    {
        gemScoreText.text = newText;
        ScaleTweenerEffect(gemScoreTransform, 1f, 0.1f, 0.8f, Ease.Flash);
        gemScoreTransform.DOScale(1f, 0.1f).From(0.8f).SetEase(Ease.InFlash);
    }

    public void SetFinalScoreText(string newText)
    {
        finalScoreText.text = newText;
        ScaleTweenerEffect(finalScoreText.rectTransform, 1f, 0.1f, 1.4f, finalScoreTextEase);
    }

    public void OnSettingButtonPressed()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        ScaleTweenerEffect(settingButtonTransform, 1.5f, 0.25f, 1f, mainMenuPanelEase);
        PanelsTweenerEffect(mainMenuPanel,true);
        PanelsTweenerEffect(settingPanel);
    }

   
    public void InGameTextTweener()
    {
        int index = Random.Range(0, feedBackText.Length);
       
        feedBackText[index].gameObject.SetActive(true);
        feedBackText[index].DOAnchorPos(new Vector2(0, 500f), 0.8f).SetEase(mainMenuPanelEase).OnComplete(() =>
        {
            feedBackText[index].DOAnchorPos(new Vector2(2000f, 500f), 0.8f).SetEase(mainMenuPanelEase);
            feedBackText[index].DOAnchorPos(new Vector2(-2000f, 500f), 0).SetDelay(0.8f);
          
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
        radialLevelCompleteBG.DORotate(new Vector3(0f, 0f, 270f), 2f).SetLoops(-1, LoopType.Yoyo);;
        
        PanelsTweenerEffect(levelCompletePanel);

      // SetFinalScore(ScoreManager.instance.GetCurrentScore().ToString());
        StartCoroutine(PopupGemEffect());
       
    }

    IEnumerator PopupGemEffect()
    {
        yield return new WaitForSeconds(1f);
        int index = 0;

        float initialScore = 0f;
        float levelScore = ScoreManager.instance.GetScore();
        float scoreTextScore = levelScore;
        while (index < ScoreManager.instance.GetScore())
        {
            RectTransform gemObj = Instantiate(levelCompleteGemImg,gemHolder.transform);
            if (scoreTextScore > 0)
            {
                scoreTextScore -= 1f;
                gemScoreText.text = scoreTextScore.ToString();
            }
            gemObj.DOMove(gemTargetPos.position, 1f).From(gemInitialPos.position + new Vector3(Random.Range(-32f, 32f), Random.Range(16, 64f), 0f)) 
                .SetEase(Ease.Linear).OnComplete(()=>
                {
                    gemObj.gameObject.SetActive(false);
                    
                    if (initialScore < levelScore)
                    {
                        
                        initialScore += 1f;
                       
                        SetFinalScoreText("x " + initialScore * ScoreManager.instance.GetMultipliedScore());

                    }
                });
            index++;
            yield return new WaitForSeconds(0.1f);
        }
        nextButtonTransform.gameObject.SetActive(true);
        nextButtonTransform.DOScale(1.25f, 1f).From(1f).SetEase(nextButtonEase).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitForSeconds(1f);
        nextButtonImg.DOFade(1f, 0.25f).From(0f);
        victoryBannerCanvasGroup.DOFade(1, 0.25f).From(0f);
    }

    public void SetPowerMeter(float newVal)
    {
        powerSlider.DOFillAmount(newVal / 3f, 0.3f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            if (powerSlider.fillAmount >= 0.9f)
            {
                powerSlider.DOColor(Color.red, 0.3f);
                maxPowerIndicator.SetActive(true);
            } 
            else if (powerSlider.fillAmount >= 0.66)
            {
                powerSlider.DOColor(Color.yellow, 0.3f);
                maxPowerIndicator.SetActive(false);
            }
            else
            {
                powerSlider.DOColor(Color.green, 0.3f);
                maxPowerIndicator.SetActive(false);
            }

        });

       
    }

    public void ResetPowerMeter()
    {
        powerSlider.DOFillAmount(0f, 0.3f).SetEase(Ease.InCubic);
       
    }
    
   
    void PanelsTweenerEffect(CanvasGroup canvasGroup, bool isOn = false)
    {
        if (!isOn)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1f, 0.75f).From(0f);
            canvasGroup.transform.DOScale(Vector3.one, 0.75f).From(new Vector3(0.75f, 0.75f)).SetEase(mainMenuPanelEase);
        }
        else
        {
            canvasGroup.DOFade(0f, 0.75f).From(1f).OnComplete(()=>canvasGroup.gameObject.SetActive(false));
        }
    }

    void ScaleTweenerEffect(RectTransform rectTransform, float endVal, float duration, float fromVal, Ease ease)
    {
        rectTransform.DOScale(endVal, duration).From(fromVal).SetEase(ease);
        
    }
    
    
    public void OnGameOver()
    {
        PanelsTweenerEffect(gameOverPanel);
    }
}
