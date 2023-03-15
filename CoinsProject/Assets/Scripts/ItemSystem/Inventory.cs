using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{ 
    static public Inventory Self;
    public GameObject ItemConteiner;
    public GameObject ItemIconPrefab;
    List<InventoryItem> ItemList = new List<InventoryItem>();
    public InventoryStatus status = InventoryStatus.show;

    private void Start() {
        Inventory.Self = this;
        Inventory.hideInventory();
    }


    static public void hideInventory(){
        if (Self.status==InventoryStatus.hide) return;

        Self.transform.gameObject.SetActive(false);
        Self.status= InventoryStatus.hide;
        GameMeneger.playGame();
    }
    static public void showInventory(){
        if (Self.status==InventoryStatus.show) return;

        Self.transform.gameObject.SetActive(true);
        Self.status = InventoryStatus.show;
        GameMeneger.pauseGame();

        Self.render();
    }
    static public void togleInventory(){
        if (Self.status==InventoryStatus.show){
            Inventory.hideInventory();
        }else{
            Inventory.showInventory();
        }
    }

    static public void addItem(InventoryItem item){
        List<InventoryItem> ItemList = Self.ItemList;

        InventoryItem itemInList = 
        ItemList.Find(elem => elem.item.name==item.item.name);

        if (itemInList == null){
            ItemList.Add(new InventoryItem(item.item, item.count));
        }else{
            itemInList.count+=item.count;
        }

        if (Self.status==InventoryStatus.show){
            Self.render();
        }
    }

    static public void removeItem(InventoryItem item, int count){
        item.count-=count;

        if (item.count==0){
            List<InventoryItem> ItemList = Self.ItemList;
            ItemList.Remove(item);
        }

        if (Self.status==InventoryStatus.show){
            Self.render();
        }
    }

    void render(){
        removeChildrens();
        if (ItemList.Count<=0) return;
        for(int i=0; i<ItemList.Count;i++){
            ItemIcon.renderOneItem(ItemList[i],ItemConteiner,ItemParent.inventory);
        }
    }

    void removeChildrens(){
        int count = this.ItemConteiner.transform.childCount;

        for(int i=count-1;i>=0;i--){
            Transform child = 
            Self.ItemConteiner.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
    
}

[System.Serializable]
public enum InventoryStatus{
    show,
    hide,
}

public enum ItemParent{
    bank,
    inventory,
}

public class InventoryItem
{
    public int count;
    public Item item;

    public InventoryItem(Item item, int count){
        this.item = item;
        this.count = count;
    }
}
