using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankWindow : MonoBehaviour
{
    static public BankWindow Self;
    public GameObject ItemConteiner;
    public GameObject ItemIconPrefab;
    public InventoryStatus status = InventoryStatus.show;

    Bank connectionBank;

    private void Start() {
        BankWindow.Self = this;
        hideBankWindow();
    }

    

    static public void hideBankWindow(){
        Self.transform.gameObject.SetActive(false);
        Self.status= InventoryStatus.hide;
        GameMeneger.playGame();
    }
    static public void showBankWindow(){
        Self.transform.gameObject.SetActive(true);
        Self.status = InventoryStatus.show;
        GameMeneger.pauseGame();

        Self.render();
    }
    static public void togleBankWindow(){
        if (Self.status==InventoryStatus.show){
            Inventory.hideInventory();
        }else{
            Inventory.showInventory();
        }
    }




    void render(){
        removeChildrens();
        renderMassIndicator();
        renderBankName();

        List<InventoryItem> ItemList = connectionBank.ItemList;

        if (ItemList.Count<=0) return;
        for(int i=0; i<ItemList.Count;i++){
            renderOneChild(ItemList[i]);
        }
    }

    void removeChildrens(){
        int count = this.ItemConteiner.transform.childCount;

        for(int i=count-1;i>=0;i--){
            Transform child = ItemConteiner.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }
   
    void renderOneChild(InventoryItem InvInem){
        GameObject child = Object.Instantiate(ItemIconPrefab,ItemConteiner.transform);
        Transform childTransform = child.transform;

        Transform textComp = childTransform.GetChild(3);
        Transform imageComp = childTransform.GetChild(1);
        
        TMP_Text text = textComp.GetComponent<TMPro.TMP_Text>();
        text.text=$"{InvInem.count}";

        Image image = imageComp.GetComponent<Image>();
        image.sprite = InvInem.item.itemImage;
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
