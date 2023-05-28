using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using ItemSystemTypes;

public class LootManager : MonoBehaviour
{
    public static void LootSmallCoins(GameObject gameObject){
        InventoryR inventory = ItemManager.Self.Inventory;
        ItemOnInventoryR newItem = new ItemOnInventoryR{
            count = 1,
            item = ItemDB.Self.itemListDB.Find((elem)=>elem.id == 0)
        };

        int freeCount = inventory.HowManyCanAddItem(newItem);
        if (freeCount == 0) return;

        inventory.AddItem(newItem);
        ScoreMeneger.AddCoins(1);
        Destroy(gameObject);
    }
    public static void LootLargeCoins(GameObject gameObject){
        InventoryR inventory = ItemManager.Self.Inventory;
        ItemOnInventoryR newItem = new ItemOnInventoryR{
            count = 1,
            item = ItemDB.Self.itemListDB.Find((elem)=>elem.id == 1)
        };

        int freeCount = inventory.HowManyCanAddItem(newItem);
        if (freeCount == 0) return;

        inventory.AddItem(newItem);
        ScoreMeneger.AddCoins(2);
        Destroy(gameObject);
    }

}
