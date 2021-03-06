using System;
using DG.Tweening;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score=0;
    public static int scoreMultiplier;

    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreMultiplier = 0;
    }

    public int AddScore(int newScore)
    {
        score += newScore;
        return score;
    }

    public int GetScore()
    {
        return score;
    }
    
   
    public int GetMultipliedScore()
    {
       
        return scoreMultiplier;
    }

    public void SetMultiliedScore()
    {
        scoreMultiplier++;
    }

}
