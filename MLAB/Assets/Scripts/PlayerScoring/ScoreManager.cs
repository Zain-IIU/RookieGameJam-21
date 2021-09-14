using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score=0;
    public static int scoreMultiplier = 0;

    

    private void Awake()
    {
        instance = this;
    }

    public int AddScore(int newScore)
    {
        score += newScore;
        return score;
    }
    
   
    public int GetCurrentScore()
    {
        return score*scoreMultiplier;
    }

    public void SetMultiliedScore()
    {
        scoreMultiplier++;
    }

}
