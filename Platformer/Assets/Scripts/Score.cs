using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    public static int score;
    public static int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    public static void Add(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Score : {score}");

        if(score > highScore)
        {
           highScore = score;

          PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
