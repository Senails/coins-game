using System;
using System.Collections.Generic;


using ItemSystemTypes;
using SaveAndLoadingTypes;
using static ItemSystemUtils;


public class ItemPanel: ItemListConteiner
{
    public static ItemPanel Self;

    public bool CanDrop { get; set; } = true;
    public bool CanPlace { get; set; } = false;
    public bool CanTake { get; set; } = false;


    public ItemOnInventoryR[] ItemArray { get; set; }
    public event Action OnChange;


    public ItemPanel(){
        ItemArray = new ItemOnInventoryR[5];
        for (int i = 0; i < ItemArray.Length; i++){
            ItemArray[i] = new ItemOnInventoryR();
        }
        ItemManager.Self.Inventory.OnChange += ()=>{
            RecalculateCount();
            Render();
        };
        Self = this;
    }


    public int HowManyCanAddItem(ItemOnInventoryR item){
        return 0;
    }
    public void AddItem(ItemOnInventoryR Item,ItemSlot preferSlot = null){
        RemoveClonLink(Item);
        preferSlot.Item.item = Item.item;
        RecalculateCount();
        OnChange?.Invoke();
    }
    public void RemoveItem(ItemOnInventoryR item,ItemSlot preferSlot){
        preferSlot.Item.item = null;
        RecalculateCount();
        OnChange?.Invoke();
    }
    public void Render(){
        OnChange?.Invoke();
    }
    

    public List<ItemData> GetSaveList(){
        List<ItemData> list = new List<ItemData>();

        foreach(var elem in ItemArray){
            int ID = elem.item!=null?elem.item.id:-1;
            list.Add(new ItemData{
                itemID = ID, 
                count = elem.count
            });
        }

        return list;
    }
    public void LoadSaveList(List<ItemData> list){
        int i = 0;
        foreach(var elem in ItemArray){
            elem.count = list[i].count;

            if (list[i].itemID!=-1){
                elem.item = ItemDB.Self.itemListDB[list[i].itemID];
            }else{
                elem.item = null;
            }
            i++;
        }
        OnChange?.Invoke();
    }


    public void RemoveClonLink(ItemOnInventoryR Item){
        foreach(ItemOnInventoryR elem in ItemArray){
            if (elem.item != null && elem.item.id == Item.item.id){
                elem.item = null;
                elem.count = 0;
            }
        }
    }
    public void UseItemInSlot(int index){
        ItemR item = ItemArray[index].item;
        if (item==null || ItemArray[index].count==0) return;

        if (item.OnUseAction!=null){
            item.OnUseAction?.Invoke();
            ItemOnInventoryR Item = new ItemOnInventoryR();
            Item.count = 1;
            Item.item = item;
            ItemManager.Self.Inventory.RemoveItem(Item,null);
        }
    }
    public void RecalculateCount(){
        ItemOnInventoryR[] invItemArray = ItemManager.Self.Inventory.ItemArray;

        foreach(ItemOnInventoryR Item in ItemArray){
            if (Item.item == null) continue;
            Item.count = FindCountItemsInConteiner(ItemManager.Self.Inventory,Item.item.id);
        }
    }
    
}