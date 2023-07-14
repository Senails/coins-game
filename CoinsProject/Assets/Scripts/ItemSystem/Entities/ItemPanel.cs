using System;
using System.Collections.Generic;
using UnityEngine;


using ItemSystemTypes;
using static ItemSystemUtils;


public class ItemPanel: ItemListConteiner
{
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
    
    
    public void Render(){
        OnChange?.Invoke();
    }
}