using System;
using UnityEngine;
using TMPro;
using ItemSystemTypes;

public class ItemPanelWindow : MonoBehaviour
{
    public GameObject SlotConteiner;
    public GameObject PanelSlotPrefab;
    private ItemPanel _itemPanel;


    private void Start() {
        _itemPanel = new ItemPanel();
        _itemPanel.OnChange+=()=>{
            Debug.Log("OnChange");
            Render();
        };
        Render();
    }
    public void Render(){
        RemoveSlots();
        ItemOnInventoryR[] arr = _itemPanel.ItemArray;

        // int i = 0;
        foreach(var Item in arr){
            GameObject panelSlot = UnityEngine.Object.Instantiate(ItemManager.Self.SlotPrefab,SlotConteiner.transform);

            // Debug.Log(panelSlot);

            // ItemPanelSlot slot = panelSlot.GetComponent<ItemPanelSlot>();
            ItemSlot itemSlot = panelSlot.GetComponent<ItemSlot>();
            itemSlot.Init(Item,_itemPanel);
            // int j = i++;
            // slot.SetText($"{j+1}");
            // itemSlot.OnLeftClick = ()=>{
            //     _itemPanel.UseItemInSlot(j);
            // };
        }
    }
    public void RemoveSlots(){
        foreach(var slot in ItemSlot.ListAllSlotsOnScreen.ToArray()){
            if (slot.Parent == _itemPanel) slot.Remove();
        }
        
    }
}