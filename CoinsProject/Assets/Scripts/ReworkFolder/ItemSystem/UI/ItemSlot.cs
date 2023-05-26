using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


using ItemSystemTypes;

public class ItemSlot : MonoBehaviour
{
    public static int StackCount = 16;
    public static List<ItemSlot> ListAllSlotsOnScreen = new List<ItemSlot>();


    public SlotParent Parent;
    public ItemOnInventoryR Item;


    public TMP_Text CountText;
    public Image ItemImage;


    public void Init(ItemOnInventoryR Item,SlotParent Parent){
        this.Parent = Parent;
        this.Item = Item;
        ListAllSlotsOnScreen.Add(this);
        Render();
    }
    public void Remove(){
        ListAllSlotsOnScreen.Remove(this);
        GameObject.Destroy(this.gameObject);
    }


    private void Render(){
        if (Item.count==0){
            RenderFreeSlot();
            return;
        }
        RenderSlotWhithItem();
    }
    private void RenderFreeSlot(){
        for(int i=0; i<transform.childCount;i++){
            var child = transform.GetChild(i);
            child.gameObject.SetActive(i==2);
        }
    }
    private void RenderSlotWhithItem(){
        CountText.text = $"{Item.count}";
        ItemImage.sprite = Item.item.itemImage;
    }
}