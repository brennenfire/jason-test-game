using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    public static event Action<int> OnScoreChanged;

    static int score;

    public static void Add(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
        Debug.Log($"Score : {score}");
    }
}
