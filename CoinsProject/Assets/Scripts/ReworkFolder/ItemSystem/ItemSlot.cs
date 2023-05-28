using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


using ItemSystemTypes;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public static List<ItemSlot> ListAllSlotsOnScreen = new List<ItemSlot>();
    public static int StackCount = 16;
    public static bool IsMoving = false;


    public ItemListConteiner Parent;
    public ItemOnInventoryR Item;


    public TMP_Text CountText;
    public Image IconImage;


    public void OnPointerClick(PointerEventData eventData){ 
        if (Item.count==0) return;
        if (IsMoving) return;
        if (eventData.button != PointerEventData.InputButton.Right) return;
        DropOnLeftButtonClick();
    }
    public void OnPointerDown(PointerEventData eventData){ 
        if (Item.count==0) return;
        if (IsMoving) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (Input.GetKey(KeyCode.LeftControl)){
            DropWhithChoise();
            return;
        }
        DragAndDropHandler();
    }


    public void Init(ItemOnInventoryR Item,ItemListConteiner Parent){
        this.Parent = Parent;
        this.Item = Item;
        ListAllSlotsOnScreen.Add(this);
        Render();
    }
    public void Remove(){
        ListAllSlotsOnScreen.Remove(this);
        GameObject.Destroy(this.gameObject);
    }



    private void DragAndDropHandler(){
        IsMoving = true;

        Debug.Log(1);


        IsMoving = false;
    }
    private void DropWhithChoise(){
        IsMoving = true;
        ItemListConteiner another = FindAnotheConteiner();
        if (another==null) return;
        int countForDrop = another.HowManyCanAddItem(Item);

        int countRange = Math.Min(Item.count,countForDrop);
        ItemManager.Self.ActiveChoiseWindow(countRange,(num)=>{
            if (num!=0){
                ItemOnInventoryR dropingItems = new ItemOnInventoryR{
                    item = Item.item,
                    count = num,
                };


                another.AddItem(dropingItems);
                Parent.RemoveItem(dropingItems,this);
            }
            IsMoving = false;
        });
    }
    private void DropOnLeftButtonClick(){
        ItemListConteiner another = FindAnotheConteiner();
        if (another==null) return;
        int countForDrop = another.HowManyCanAddItem(Item);


        ItemOnInventoryR dropingItems = new ItemOnInventoryR{
            item = Item.item,
            count = Math.Min(Item.count,countForDrop),
        };


        another.AddItem(dropingItems);
        Parent.RemoveItem(dropingItems,this);
    }
    private ItemListConteiner FindAnotheConteiner(){
        foreach(var slot in ListAllSlotsOnScreen){
            if (slot.Parent!=Parent) return slot.Parent;
        }
        return null;
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
        IconImage.sprite = Item.item.itemImage;
    }
}