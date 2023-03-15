
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

        if (Input.GetMouseButtonDown(1)){
            moveOnRightClick();
            return;
        }

        if (!Input.GetMouseButtonDown(0)) return;

        DropableZone.dragStart(this.gameObject,(x,y)=>{
            if (parent==ItemParent.inventory){
                if (BankWindow.Self.status == InventoryStatus.hide) return;
                bool res = DropableZone.checkDropOnRect(x,y,BankWindow.Self.gameObject);
                if (!res) return;
                tryMoveToBank();
            }else{
                if (Inventory.Self.status == InventoryStatus.hide) return;
                bool res = DropableZone.checkDropOnRect(x,y,Inventory.Self.gameObject);
                if (!res) return;
                tryMoveToInventory();
            }
        });

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

    void tryMoveToBank(){
        Bank bank =  BankWindow.Self.connectionBank;
        int freeMass = bank.maxMass-bank.activeMass;

        int massOneItem = item.item.mass;

        if (freeMass>massOneItem*item.count){
            BankWindow.addItem(item);
            Inventory.removeItem(item,item.count);
            return;
        }

        int enalbeCount = freeMass/massOneItem;

        if (enalbeCount==0) return;

        BankWindow.addItem(new InventoryItem(item.item,enalbeCount));
        Inventory.removeItem(item,enalbeCount);
    }
    void tryMoveToInventory(){
        Inventory.addItem(item);
        BankWindow.removeItem(item,item.count);
    }
    
    void moveOnRightClick(){
        if (parent==ItemParent.inventory){
            if (BankWindow.Self.status == InventoryStatus.hide) return;
            tryMoveToBank();
        }else{
            if (Inventory.Self.status == InventoryStatus.hide) return;
            tryMoveToInventory();
        }
    }
}
