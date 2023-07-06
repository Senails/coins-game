using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


using ItemSystemTypes;
using static UiCordsLib;
using static MyDateLib;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public static List<ItemSlot> ListAllSlotsOnScreen = new List<ItemSlot>();
    public static int StackCount = 16;
    public static bool IsMoving = false;


    public ItemListConteiner Parent;
    public ItemOnInventoryR Item;


    public TMP_Text CountText;
    public Image IconImage;

    public Action OnLeftClick;


    public void OnPointerClick(PointerEventData eventData){ 
        if (Item.count==0 || IsMoving) return;
        if (eventData.button == PointerEventData.InputButton.Right){
            ReplaceOnRightButtonClick();
        }
    }
    public void OnPointerDown(PointerEventData eventData){
        if (Item.count==0 || IsMoving) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (Input.GetKey(KeyCode.LeftControl)){
            ReplaceWhithChoise();
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
    public void OnDisable() {
        Remove();
    }

    private void DragAndDropHandler(){
        Vector2 mP = findMousePositionInCanvas();
        long startDate = getDateMilisec();

        IsMoving = true;
        ItemManager.Self.GragAndDropZone.dragStart(this.gameObject,(vec2)=>{
            IsMoving = false;

            long endDate = getDateMilisec();
            long deltaDate = endDate - startDate;

            if ((vec2-mP).magnitude<3 && deltaDate<200){
                OnLeftClick?.Invoke();
                return;
            }

            ItemSlot dropSlot = FindDropEndSlot(vec2.x,vec2.y);
            
            if (dropSlot==null) return;
            if (dropSlot==this) return;
            if (dropSlot.Parent == Parent){
                DropInMyParent(dropSlot);
                return;
            }

            if (!Parent.CanTake) return;
            if (!dropSlot.Parent.CanDrop) return;
            DropInAnotherParent(dropSlot);
        });
    }
    private ItemSlot FindDropEndSlot(float x,float y){
        Debug.Log(ListAllSlotsOnScreen.Count);
        foreach(var slot in ListAllSlotsOnScreen){
            if (CheckCordsInUIRect(x,y,slot.gameObject)) return slot;
        }
        return null;
    }
    private void DropInMyParent(ItemSlot toSlot){
        if (toSlot.Item.count==0){
            //replace item in inventory
            toSlot.Item.count = Item.count;
            toSlot.Item.item = Item.item;
            Item.count = 0;
            Item.item = null;
        }else if (toSlot.Item.item.id != Item.item.id){
            //swap item in inventory
            ItemR myItem = Item.item;
            int myCount = Item.count;

            Item.item = toSlot.Item.item;
            Item.count = toSlot.Item.count;

            toSlot.Item.item = myItem;
            toSlot.Item.count = myCount;
        }else{
            //stack item in inventory
            int itemsForStack = Math.Min((StackCount - toSlot.Item.count),Item.count);
            toSlot.Item.count+=itemsForStack;
            Item.count-=itemsForStack;
        }
        Parent.Render();
    }
    private void DropInAnotherParent(ItemSlot toSlot){
        int dropItemsCount = toSlot.Parent.HowManyCanAddItem(Item);

        ItemOnInventoryR dropingItems = new ItemOnInventoryR{
            item = Item.item,
            count = Math.Min(Item.count,dropItemsCount),
        };

        toSlot.Parent.AddItem(dropingItems,toSlot);
        Parent.RemoveItem(dropingItems,this);
    }


    private void ReplaceWhithChoise(){
        ItemListConteiner another = FindAnotheConteiner();

        if (!Parent.CanTake) return;
        if (another==null) return;

        int countForDrop = another.HowManyCanAddItem(Item);
        if (countForDrop==0) return;

        int countRange = Math.Min(Item.count,countForDrop);

        IsMoving = true;
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
    private void ReplaceOnRightButtonClick(){
        ItemListConteiner another = FindAnotheConteiner();

        if (!Parent.CanTake) return;
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
            if (slot.Parent!=Parent && slot.Parent.CanPlace == true) return slot.Parent;
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