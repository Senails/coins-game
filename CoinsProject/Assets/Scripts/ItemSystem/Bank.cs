using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public string bankName;
    public int maxMass;
    public int activeMass;
    public List<InventoryItem> ItemList = new List<InventoryItem>();

    bool connect = false;

    private void Update() {
        if (!connect) return;
        if (BankWindow.Self.status == InventoryStatus.show) return;
        if (Input.GetKeyDown(KeyCode.E) 
        || Input.GetMouseButton(1)){
            connectWithUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HintScript.connect();
        this.connect = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        HintScript.disconnect();
        this.connect = false;
    }

    void connectWithUI(){
        BankWindow.connectWhithBank(this);
    }

}
