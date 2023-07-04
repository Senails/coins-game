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
            Render();
        };
        Render();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            _itemPanel.UseItemInSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            _itemPanel.UseItemInSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            _itemPanel.UseItemInSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            _itemPanel.UseItemInSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)){
            _itemPanel.UseItemInSlot(4);
        }
    }

    public void Render(){
        RemoveSlots();
        ItemOnInventoryR[] arr = _itemPanel.ItemArray;

        int i = 0;
        foreach(var Item in arr){
            GameObject panelSlot = UnityEngine.Object.Instantiate(PanelSlotPrefab,SlotConteiner.transform);

            ItemPanelSlot slot = panelSlot.GetComponent<ItemPanelSlot>();
            ItemSlot itemSlot = slot.ItemSlotObject;
            itemSlot.Init(Item,_itemPanel);

            int j = i++;
            slot.Init($"{j+1}");
            itemSlot.OnLeftClick = ()=>{
                _itemPanel.UseItemInSlot(j);
            };
        }
    }
    public void RemoveSlots(){
        foreach(var slot in ItemSlot.ListAllSlotsOnScreen.ToArray()){
            if (slot.Parent == _itemPanel) slot.Remove();
        }
        for(int i = SlotConteiner.transform.childCount-1;i>-1;i--){
            GameObject.Destroy(SlotConteiner.transform.GetChild(i).gameObject);
        }
    }
}