
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerDownHandler
{
    public InventoryItem item;
    public ItemParent parent;


    public void OnPointerDown(PointerEventData eventData){
        if (DropableZone.Self.droping) return;
        DropableZone.dragStart(this.gameObject);
    }

    static public GameObject renderOneItem( InventoryItem InvInem, GameObject parent, ItemParent owner){
        GameObject ItemIconPrefab = ItemDataBase.Self.ItemIconPrefab;

        GameObject child = Object.Instantiate(ItemIconPrefab,parent.transform);
        Transform childTransform = child.transform;

        Transform textComp = childTransform.GetChild(3);
        Transform imageComp = childTransform.GetChild(1);
        
        TMP_Text text = textComp.GetComponent<TMPro.TMP_Text>();
        text.text=$"{InvInem.count}";

        Image image = imageComp.GetComponent<Image>();
        image.sprite = InvInem.item.itemImage;

        ItemIcon childScript= child.GetComponent<ItemIcon>();
        childScript.item=InvInem;
        childScript.parent=owner;

        return child;
    }
}
