using System;
using System.Collections.Generic;
using UnityEngine;


using ItemSystemTypes;
using static ItemSystemUtils;


public class InventoryR: ItemListConteiner
{
    public bool CanDrop { get; set; } = true;
    public bool CanPlace { get; set; } = true;
    public bool CanTake { get; set; } = true;


    public ItemOnInventoryR[] ItemArray { get; set; }
    public event Action OnChange;


    public int SlotsCount = 30;


    public InventoryR(){
        ItemArray = new ItemOnInventoryR[SlotsCount];
        for (int i = 0; i < ItemArray.Length; i++){
            ItemArray[i] = new ItemOnInventoryR();
        }
    }


    public int HowManyCanAddItem(ItemOnInventoryR Item){
        int stackCount = ItemSlot.StackCount;
        int freePositions = 0;

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

        int result = Mathf.Clamp(freePositions,0,stackCount);


        //limit heal potions in inventory
        if (Item.item.id==2){
            int potionInInventory = FindCountItemsInConteiner(this,2);
            if (potionInInventory>9) return 0;
            result = Mathf.Min(result,10-potionInInventory);
        }

        return result;
    }
    public void AddItem(ItemOnInventoryR Item,ItemSlot preferSlot = null){
        if(Item.count==0) return;

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

        OnChange?.Invoke();
    }
    public void RemoveItem(ItemOnInventoryR Item,ItemSlot whichSlot = null){
        if(Item.count==0) return;

        ItemOnInventoryR inventorySlot = null;
        if (whichSlot==null){
            inventorySlot = FindSlotWhithItem(Item);
        }else{
            inventorySlot = whichSlot.Item;
        }

        if (Item.count>=inventorySlot.count){
            inventorySlot.count = 0;
            inventorySlot.item = null;
        }else{
            inventorySlot.count -= Item.count;
        }

        OnChange?.Invoke();
    }
    public void Render(){
        OnChange?.Invoke();
    }



    private ItemOnInventoryR FindSlotWhithItem(ItemOnInventoryR Item){
        foreach(var elem in ItemArray){
            if (elem.count == 0) continue;
            if (elem.item.id == Item.item.id) return elem;
        }
        return null;
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
}
