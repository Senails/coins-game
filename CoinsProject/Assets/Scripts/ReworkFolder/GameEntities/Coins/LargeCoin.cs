using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeCoin : MonoBehaviour
{
    private bool _connect = false;

    public void Update()
    {
        if (!_connect) return;
        if (Input.GetKey(KeyCode.E)){
            OnHoldCoin();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        _connect = true;
        HintManager.Connect();
    }

    private void OnTriggerExit2D(Collider2D other) {
        _connect = false;
        HintManager.Disconnect();
    }

    private void OnHoldCoin(){
        Destroy(gameObject);
        LootManager.LootLargeCoins();
    }
}
