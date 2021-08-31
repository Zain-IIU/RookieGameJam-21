using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStarted;
    public bool isGameOver;

    public Action OnGameStart;

    // Start is called before the first frame update
    void Awake()
    { 
        instance = this;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
