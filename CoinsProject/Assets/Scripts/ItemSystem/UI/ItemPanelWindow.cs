using System;
using UnityEngine;
using TMPro;
using ItemSystemTypes;

using static KeyUtils;

public class ItemPanelWindow : MonoBehaviour
{
    public GameObject SlotConteiner;
    public GameObject PanelSlotPrefab;
    private ItemPanel _itemPanel;


    private void Start() {
        _itemPanel = new ItemPanel();

        _itemPanel.OnChange += Render;
        OptionsManager.Self.OnChangeConfig += Render;

        Render();
    }

    private void Update() {
        var KyeDictionary = OptionsManager.Config.KyeDictionary;
        ItemOnInventoryR[] arr = _itemPanel.ItemArray;

        int i = 0;
        foreach(var Item in _itemPanel.ItemArray){
            if (Input.GetKeyDown(KyeDictionary[$"Ячейка {i+1}"])){
                _itemPanel.UseItemInSlot(i);
            }
            i++;
        }
    }


    public void Render(){
        RemoveSlots();
        var KyeDictionary = OptionsManager.Config.KyeDictionary;
        ItemOnInventoryR[] arr = _itemPanel.ItemArray;

        int i = 0;
        foreach(var Item in arr){
            GameObject panelSlot = UnityEngine.Object.Instantiate(PanelSlotPrefab,SlotConteiner.transform);

            ItemPanelSlot slot = panelSlot.GetComponent<ItemPanelSlot>();
            ItemSlot itemSlot = slot.ItemSlotObject;
            itemSlot.Init(Item,_itemPanel);

            int j = i++;

            KeyCode code = KyeDictionary[$"Ячейка {i}"];
            slot.Init(GetNameKey(code));

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