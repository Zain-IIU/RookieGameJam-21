using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score;
    
    private void Awake()
    {
        instance = this;
    }

    public int AddScore(int newScore)
    {
        score += newScore;
        return score;
    }
    
}
