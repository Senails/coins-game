using System.Collections.Generic;
using UnityEngine;

using ItemSystemTypes;

public class ItemDB : MonoBehaviour
{
    public List<ItemR> ItemListDB;
    static public ItemDB Self;


    private void Start() {
        ItemDB.Self = this;
    }
}
