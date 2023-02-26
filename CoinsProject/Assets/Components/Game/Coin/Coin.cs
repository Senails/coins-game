using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public string size = "small";

    bool connect = false;

    void Update()
    {
        if (!connect) return;
        if (Input.GetKey(KeyCode.E) || Input.GetMouseButton(1)){
            addCoin(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name != "User") return;

        if (size == "small"){
            addCoin(1);
        }else{
            this.connect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        this.connect = false;
    }

    void addCoin(int count){
        CoinCounter.addCoins(count);
        Destroy(gameObject);
    }

}
