using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankWindow : MonoBehaviour
{
    static public BankWindow Self;
    public GameObject ItemConteiner;
    public InventoryStatus status = InventoryStatus.hide;

    public Bank connectionBank;

    static public void hideBankWindow(){
        if (ChoiceQuantity.Self.status == InventoryStatus.show) return;
        Self.transform.gameObject.SetActive(false);
        Self.status = InventoryStatus.hide;
        GameMeneger.playGame();
        Inventory.hideInventory();
    }
    static public void showBankWindow(){
        Self.transform.gameObject.SetActive(true);
        Self.status = InventoryStatus.show;
        GameMeneger.pauseGame();
        Inventory.showInventory();

        Self.render();
    }
    static public void togleBankWindow(){
        if (Self.status==InventoryStatus.show){
            Inventory.hideInventory();
        }else{
            Inventory.showInventory();
        }
    }

    static public void addItem(InventoryItem item){
        List<InventoryItem> ItemList = Self.connectionBank.ItemList;

        InventoryItem itemInList = 
        ItemList.Find(elem => elem.item.name==item.item.name);

        if (itemInList == null){
            ItemList.Add(new InventoryItem(item.item, item.count));
        }else{
            itemInList.count+=item.count;
        }

        Self.connectionBank.activeMass+=item.count*item.item.mass;

        if (Self.status==InventoryStatus.show){
            Self.render();
        }
    }

    static public void removeItem(InventoryItem item, int count){
        item.count-=count;
        Self.connectionBank.activeMass-=item.item.mass*count;

        if (item.count==0){
            List<InventoryItem> ItemList = Self.connectionBank.ItemList;
            ItemList.Remove(item);
        }

        if (Self.status==InventoryStatus.show){
            Self.render();
        }
    }

    void render(){
        removeChildrens();
        renderMassIndicator();
        renderBankName();

        List<InventoryItem> ItemList = connectionBank.ItemList;

        if (ItemList.Count<=0) return;
        for(int i=0; i<ItemList.Count;i++){
            ItemIcon.renderOneItem(ItemList[i],ItemConteiner,ItemParent.bank);
        }
    }

    void removeChildrens(){
        int count = this.ItemConteiner.transform.childCount;

        for(int i=count-1;i>=0;i--){
            Transform child = ItemConteiner.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
   
    void renderMassIndicator(){
        Transform parent = transform.GetChild(4);
        Transform progressLine = parent.GetChild(0);
        Transform textBlock = transform.GetChild(3);

        RectTransform rectTr = progressLine.GetComponent<RectTransform>();

        int activeMass = connectionBank.activeMass;
        int maxMass = connectionBank.maxMass;


        float parentWidth = parent.GetComponent<RectTransform>().rect.size.x;
        float needWidth = ((float)activeMass/(float)maxMass)*parentWidth;

        rectTr.sizeDelta = new Vector2(needWidth,rectTr.rect.size.y);

        int percent = (int)Mathf.Floor(((float)activeMass/(float)maxMass)*100);

        TMP_Text text = textBlock.GetComponent<TMPro.TMP_Text>();
        text.text=$"{percent}%";
    }
   
    void renderBankName(){
        Transform textBlock = transform.GetChild(1);
        TMP_Text text = textBlock.GetComponent<TMPro.TMP_Text>();

        text.text=$"{connectionBank.bankName}";
    }


    static public void connectWhithBank(Bank bank){
        Self.connectionBank=bank;
        showBankWindow();
    }
}
