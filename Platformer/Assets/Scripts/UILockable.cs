using UnityEngine;

public class UILockable : MonoBehaviour
{
    void OnEnable()
    {
        //PlayerPrefs.DeleteAll();
        var startButton = GetComponent<UIStartLevelButton>();
        string key = startButton.LevelName + "Unlocked";
        int unlocked = PlayerPrefs.GetInt(key);
        if(unlocked == 0)
        {
            gameObject.SetActive(false);
        }
    }

    [ContextMenu("Delete unlock prefs")]
    public void ClearLevelUnlocked()
    {
        PlayerPrefs.DeleteAll();
    }
}
