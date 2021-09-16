using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStarted;
    public bool isLevelCompleted;
    public bool isGameOver;

    public Action OnGameStart;

    [SerializeField] private GameObject levelCompleteFx;
    [SerializeField] private CinemachineVirtualCamera levelCompleteCam;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    
    // Start is called before the first frame update
    void Awake()
    { 
        instance = this;
        cinemachineBrain.m_DefaultBlend.m_Time = 0.25f;
        // todo checking to see if UI is blocking touches
        Input.simulateMouseWithTouches = true;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (!isGameStarted && Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            OnGameStart?.Invoke();
        }
    }
    
    public void OnRestartButtonPress()
    {
        /*SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
    }

    public void LevelCompleted()
    {
        cinemachineBrain.m_DefaultBlend.m_Time = 1f;
        isLevelCompleted = true;
        UIManager.instance.OnLevelComplete();
        levelCompleteFx.SetActive(true);
        levelCompleteCam.gameObject.SetActive(true);
        levelCompleteCam.Priority = 25;
    }
}
