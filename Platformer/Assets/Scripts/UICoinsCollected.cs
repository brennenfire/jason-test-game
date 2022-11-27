using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoinsCollected : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.SetText(Coin.CoinsCollected.ToString());
    }
}
