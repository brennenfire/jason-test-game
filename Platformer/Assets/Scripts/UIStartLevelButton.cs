using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] string levelName;

    private void OnValidate()
    {
        GetComponentInChildren<TMP_Text>()?.SetText(levelName);
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
