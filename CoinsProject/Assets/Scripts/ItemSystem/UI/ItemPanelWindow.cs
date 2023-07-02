using System;
using UnityEngine;
using TMPro;
using ItemSystemTypes;

public class ItemPanelWindow : MonoBehaviour
{
    public GameObject SlotConteiner;
    private ItemPanel _itemPanel;


    private void Start() {
        _itemPanel = new ItemPanel();
        _itemPanel.OnChange+=()=>{
            Render();
        };
        Render();
    }
    private void Render(){
        RemoveSlots();
        ItemOnInventoryR[] arr = _itemPanel.ItemArray;

        foreach(var Item in arr){
            GameObject slot = UnityEngine.Object.Instantiate(ItemManager.Self.SlotPrefab,SlotConteiner.transform);
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            itemSlot.Init(Item,_itemPanel);
        }
    }
    private void RemoveSlots(){
        foreach(var slot in ItemSlot.ListAllSlotsOnScreen.ToArray()){
            if (slot.Parent==_itemPanel) slot.Remove();
        }
    }
}