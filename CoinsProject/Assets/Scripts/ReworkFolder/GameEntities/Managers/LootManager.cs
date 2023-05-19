using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static void LootSmallCoins(){
        List<Item> itemDB = ItemDataBase.Self.ItemDB;
        Item item = itemDB.Find(item=> item.name == "Small_Coin");
        if (item==null) return;
        Inventory.addItem(new InventoryItem(item,1));
    }
    public static void LootLargeCoins(){
        List<Item> itemDB = ItemDataBase.Self.ItemDB;
        Item item = itemDB.Find(item=> item.name == "Large_Coin");
        if (item==null) return;
        Inventory.addItem(new InventoryItem(item,1));
    }

}
