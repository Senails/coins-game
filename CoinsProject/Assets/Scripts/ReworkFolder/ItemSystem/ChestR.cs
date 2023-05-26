using System;
using System.Collections.Generic;
using UnityEngine;


using ItemSystemTypes;


public class ChestR
{
    public string Name = "Chest999";
    public int MaxMass = 10;
    public int ActiveMass = 5;
    public int SlotsCount = 25;


    public ItemOnInventoryR[] ItemArray;
    public Action OnChange;


    public ChestR(){
        ItemArray = new ItemOnInventoryR[SlotsCount];
        for (int i = 0; i < ItemArray.Length; i++){
            ItemArray[i] = new ItemOnInventoryR();
        }
    }

}
