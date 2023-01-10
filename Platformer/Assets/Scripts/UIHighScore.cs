using TMPro;
using UnityEngine;

public class UIHighScore : MonoBehaviour
{
    TMP_Text text;

    private void OnValidate()
    {
        text = GetComponent<TMP_Text>();
        UpdateScoreText(PlayerPrefs.GetInt("HighScore"));
    }

    private void UpdateScoreText(int score)
    {
        text.SetText(score.ToString());
    }

}
