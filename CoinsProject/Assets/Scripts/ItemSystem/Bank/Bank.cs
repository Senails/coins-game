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
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(1)){
            if (BankWindow.Self.status == InventoryStatus.show){
                if (!Input.GetKeyDown(KeyCode.E)) return;
                BankWindow.hideBankWindow();
            }else{
                connectWithUI();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        HintManager.Connect();
        this.connect = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        HintManager.Disconnect();
        this.connect = false;
    }

    void connectWithUI(){
        BankWindow.connectWhithBank(this);
    }

}
