using System.Collections.Generic;
using UnityEngine;

using ItemSystemTypes;

public class ItemDB : MonoBehaviour
{
    public List<ItemR> itemListDB;
    public static ItemDB Self;


    private void Start() {
        ItemDB.Self = this;
    }
}
