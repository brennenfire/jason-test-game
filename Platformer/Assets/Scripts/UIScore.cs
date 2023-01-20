using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        Score.OnScoreChanged += UpdateScoreText;
        UpdateScoreText(Score.Scoree);
    }

    void OnDestroy()
    {
        Score.OnScoreChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        text.SetText(score.ToString());
    }
}
