using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCoin : MonoBehaviour
{
    private bool _isLooted = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player") return;
        if (_isLooted) return;
        _isLooted = true;
        LootManager.LootSmallCoins(gameObject);
    }
}
