using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemPanelSlot : MonoBehaviour
{
    public static List<ItemPanelSlot> ListAllPanelSlots = new List<ItemPanelSlot>();

    public ItemSlot ItemSlotObject;
    public TMP_Text Text;

    private void Start() {
        ListAllPanelSlots.Add(this);
    }
    public void SetText(string text){
        Text.text = text;
    }

    public void Remove(){
        ItemSlotObject.Remove();
        ListAllPanelSlots.Remove(this);
        GameObject.Destroy(this.gameObject);
    }
}
