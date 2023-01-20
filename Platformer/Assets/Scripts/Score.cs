using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;

    public static int Scoree { get; private set; }
    static int highScore;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        Scoree = 0;
    }

    public static void Add(int points)
    {
        Scoree += points;
        OnScoreChanged?.Invoke(Scoree);
        Debug.Log($"Score : {Scoree}");

        if(Scoree > highScore)
        {
           highScore = Scoree;

          PlayerPrefs.SetInt("HighScore", highScore);
        }
    }
}
