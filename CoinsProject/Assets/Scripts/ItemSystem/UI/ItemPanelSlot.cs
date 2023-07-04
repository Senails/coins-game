using System;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ItemPanelSlot : MonoBehaviour
{
    public ItemSlot ItemSlotObject;
    public TMP_Text Text;


    public Action ActivateAction;


    public void Init(string text){
        Text.text = text;
    }
}
