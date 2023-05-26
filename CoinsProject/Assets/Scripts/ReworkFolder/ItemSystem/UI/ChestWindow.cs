using System;
using UnityEngine;
using TMPro;
using ItemSystemTypes;

public class ChestWindow : MonoBehaviour
{
    public GameObject SlotConteiner;
    public GameObject SlotPrefab;


    public TMP_Text TextPercent;
    public TMP_Text ChestNameText;
    public RectTransform MassIndicatorLine;


    public void Init(){
        ItemManager.Self.ConectedChest.OnChange = Render;
        Render();
    }
    public void CancelChestWindow(){
        ItemManager.Self.CloseChestWindow();
    }


    private void Render(){
        ChestR chest = ItemManager.Self.ConectedChest;

        //render name chest
        ChestNameText.text = chest.Name;

        //render percent text
        int percentMass = (int)Mathf.Floor(((float)chest.ActiveMass/(float)chest.MaxMass)*100);
        TextPercent.text = $"{percentMass}%";

        //render percent line
        float parentWidth = MassIndicatorLine.parent.GetComponent<RectTransform>().rect.size.x;
        float needWidthLine = percentMass*parentWidth/100;
        MassIndicatorLine.sizeDelta = new Vector2(needWidthLine,MassIndicatorLine.rect.size.y);

        RenderSlots();
    }
    private void RenderSlots(){
        RemoveSlots();
        ItemOnInventoryR[] arr = ItemManager.Self.ConectedChest.ItemArray;

        foreach(var Item in arr){
            GameObject slot = UnityEngine.Object.Instantiate(SlotPrefab,SlotConteiner.transform);
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            itemSlot.Init(Item,SlotParent.chest);
        }
    }
    private void RemoveSlots(){
        foreach(var slot in ItemSlot.ListAllSlotsOnScreen.ToArray()){
            if (slot.Parent==SlotParent.chest) slot.Remove();
        }
    }
}