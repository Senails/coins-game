using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeCoin : MonoBehaviour
{
    private bool _connect = false;

    public void Update()
    {
        if (!_connect) return;
        if (Input.GetKey(OptionsManager.Config.KyeDictionary["Взаимодействие"])){
            OnHoldCoin();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player") return;
        _connect = true;
        HintManager.Connect();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag != "Player") return;
        _connect = false;
        HintManager.Disconnect();
    }

    private void OnHoldCoin(){
        LootManager.LootLargeCoins(gameObject);
    }
}
