using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int totalCoins = 3;
    
    int remainingCoins;

    protected override bool CanUse => remainingCoins> 0;
    void Start()
    {
        remainingCoins = totalCoins;    
    }

    protected override void Use()
    {
        base.Use();
        remainingCoins--;
        Coin.CoinsCollected++;
    }

}
