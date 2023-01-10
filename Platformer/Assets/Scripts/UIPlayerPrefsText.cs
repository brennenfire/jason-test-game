using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] string key;

    void OnEnable()
    {
        int value = PlayerPrefs.GetInt(key);
        GetComponent<TMP_Text>().SetText(value.ToString());
    }
}
