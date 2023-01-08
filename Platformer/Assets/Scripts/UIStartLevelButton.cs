using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] string levelName;

    //public string LevelName { get { return levelName; } }
    public string LevelName => levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
