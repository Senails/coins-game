using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataBase: MonoBehaviour
{

    public List<Item> ItemDB;

    private void Start() {
        // Item ider = ItemDB.Find(item => item.id==2);

        // Debug.Log(ider);
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