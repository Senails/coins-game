using System;
using System.Collections.Generic;
using UnityEngine;


using ItemSystemTypes;


public class ChestR: ItemListConteiner
{
    public string Name = "Chest999";
    public int MaxMass;
    public int ActiveMass = 0;
    public int SlotsCount;
    public Action OnChange;


    public ItemOnInventoryR[] ItemArray { get; set; }


    public ChestR(string name ="Chest999" , int maxMass = 10, int slotsCount = 20){
        Name = name;
        MaxMass = maxMass;
        SlotsCount = slotsCount;

        ItemArray = new ItemOnInventoryR[SlotsCount];
        for (int i = 0; i < ItemArray.Length; i++){
            ItemArray[i] = new ItemOnInventoryR();
        }
    }


    public int HowManyCanAddItem(ItemOnInventoryR Item){
        int stackCount = ItemSlot.StackCount;
        int freePositions = 0;

        //check free positions
        foreach(var itemPlace in ItemArray){
            if (itemPlace.count==0){
                freePositions += stackCount;
            }else if (itemPlace.item.id==Item.item.id){
                freePositions += (stackCount - itemPlace.count);
            }else{
                continue;
            }
            if(freePositions>=stackCount) break;
        }

        //check free mass
        int freeMassPosiions = (MaxMass - ActiveMass)/Item.item.mass;


        freePositions = Mathf.Min(freePositions,freeMassPosiions);
        return Mathf.Clamp(freePositions,0,stackCount);
    }
    public void AddItem(ItemOnInventoryR Item,ItemSlot preferSlot = null){
        int CountItemsForAdd = Item.count;

        if (preferSlot!=null){
            int addedItemsCount = TryAddItemToSlot(Item, preferSlot.Item);
            CountItemsForAdd -= addedItemsCount;
        }

        if (CountItemsForAdd!=0){
            foreach(var slot in ItemArray){
                if (slot.item == null || slot.item.id != Item.item.id) continue;
                int addedItemsCount = TryAddItemToSlot(new ItemOnInventoryR{
                    item = Item.item,
                    count = CountItemsForAdd
                }, slot);

                CountItemsForAdd -= addedItemsCount;
                if (CountItemsForAdd==0) break;
            }
        }

        if (CountItemsForAdd!=0){
            foreach(var slot in ItemArray){
                int addedItemsCount = TryAddItemToSlot(new ItemOnInventoryR{
                    item = Item.item,
                    count = CountItemsForAdd
                }, slot);
                CountItemsForAdd -= addedItemsCount;
                if (CountItemsForAdd==0) break;
            }
        }
        
        RecalculateActiveMass();
        OnChange?.Invoke();
    }
    public void RemoveItem(ItemOnInventoryR item,ItemSlot whichSlot){
        int index = Array.IndexOf(ItemArray,whichSlot.Item);
        if (item.count>=whichSlot.Item.count){
            ItemArray[index].count = 0;
            ItemArray[index].item = null;
        }else{
            ItemArray[index].count -= item.count;
        }

        RecalculateActiveMass();
        OnChange?.Invoke();
    }
    public void Render(){
        OnChange?.Invoke();
    }
    
    
    private int TryAddItemToSlot(ItemOnInventoryR Item,ItemOnInventoryR slot){
        if (slot.count!=0 && Item.item.id != slot.item.id) return 0;

        int stackCount = ItemSlot.StackCount;
        int canAdd = stackCount - slot.count;
        
        int addedItemsCount = Math.Min(Item.count,canAdd);

        slot.item = Item.item;
        slot.count+=addedItemsCount;

        return addedItemsCount;
    }
    private void RecalculateActiveMass(){
        ActiveMass = 0;
        foreach(var itemPlace in ItemArray){
            if (itemPlace.count==0) continue;
            ActiveMass += itemPlace.count*itemPlace.item.mass;
        }
    }
}
