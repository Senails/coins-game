using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    static public Inventory Self;

    private InventoryStatus status = InventoryStatus.show;

    private void Start() {
        Inventory.Self = this;
        Inventory.hideInventory();
    }

    static public void hideInventory(){
        Self.transform.gameObject.SetActive(false);
        Self.status= InventoryStatus.hide;

        Debug.Log("hide inventory");
    }
    static public void showInventory(){
        Self.transform.gameObject.SetActive(true);
        Self.status = InventoryStatus.show;

        Debug.Log("show inventory");
    }
    static public void togleInventory(){
        if (Self.status==InventoryStatus.show){
            Inventory.hideInventory();
        }else{
            Inventory.showInventory();
        }
    }

}

[System.Serializable]
enum InventoryStatus{
    show,
    hide,
}
