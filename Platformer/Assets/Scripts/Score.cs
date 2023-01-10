using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Score
{
    static int score;

    public static void Add(int points)
    {
        score += points;
        Debug.Log($"Score : {score}");
    }
}
