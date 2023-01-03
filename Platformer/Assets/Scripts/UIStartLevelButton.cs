using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] string levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
