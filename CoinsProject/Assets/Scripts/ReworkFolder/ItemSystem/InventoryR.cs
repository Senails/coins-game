using System;
using System.Collections.Generic;
using UnityEngine;


using ItemSystemTypes;


public class InventoryR
{
    public int SlotsCount = 30;
    public ItemOnInventoryR[] ItemArray;
    public Action OnChange;

    
    public InventoryR(){
        ItemArray = new ItemOnInventoryR[SlotsCount];
        for (int i = 0; i < ItemArray.Length; i++){
            ItemArray[i] = new ItemOnInventoryR();
        }
    }

}
