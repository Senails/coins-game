using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    static public Inventory Self;
    public GameObject ItemConteiner;
    public GameObject ItemIconPrefab;
    List<InventoryItem> ItemList = new List<InventoryItem>();
    private InventoryStatus status = InventoryStatus.show;

    private void Start() {
        Inventory.Self = this;
        Inventory.hideInventory();
    }


    static public void hideInventory(){
        Self.transform.gameObject.SetActive(false);
        Self.status= InventoryStatus.hide;
        GameMeneger.playGame();
    }
    static public void showInventory(){
        Self.transform.gameObject.SetActive(true);
        Self.status = InventoryStatus.show;
         GameMeneger.pauseGame();
    }
    static public void togleInventory(){
        if (Self.status==InventoryStatus.show){
            Inventory.hideInventory();
        }else{
            Inventory.showInventory();
        }
    }

    static public void addItem(Item item , int count){
        InventoryItem itemInList = 
        Self.ItemList.Find(elem => elem.item.name==item.name);

        if (itemInList == null){
            Self.ItemList.Add(new InventoryItem(item,count));
        }else{
            itemInList.count+=count;
        }
    }
    static public void removeItem(Item item , int count){
        
    }
}

[System.Serializable]
enum InventoryStatus{
    show,
    hide,
}

class InventoryItem
{
    public int count;
    public Item item;

    public InventoryItem(Item item, int count){
        this.item = item;
        this.count = count;
    }
}
