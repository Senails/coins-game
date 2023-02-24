using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinCounter : MonoBehaviour
{
    int countCoins;
    static CoinCounter Self;
    public TMP_Text textBlock;

    private void Start() {
        Self = this;
        countCoins=0;
        textBlock.text = countCoins.ToString();
    }

    static public void addCoins(int count){
        Self.countCoins+=count;
        Self.textBlock.text = Self.countCoins.ToString();
    }
}
