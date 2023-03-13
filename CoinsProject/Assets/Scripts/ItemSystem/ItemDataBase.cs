using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataBase: MonoBehaviour
{

    public List<Item> ItemDB;
    static public ItemDataBase Self;

    private void Start() {
        ItemDataBase.Self = this;
    }
}

[System.Serializable]
public class Item 
{
    public int id;
    public string name;
    public Sprite itemImage;

    public int mass;
}