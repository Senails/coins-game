using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{   
    public InventoryR Inventory = new InventoryR();
    public ChestR ConectedChest;

    public DropableZone GragAndDropZone;


    public GameObject SlotPrefab;
    public InventoryWindow InventoryWin;
    public ChestWindow ChestWin;
    public ChoiseWindow ChoiseWin;
    public static ItemManager Self;


    private void Start() {
        Self = this;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.I)){
            TogleInventoryWindow();
        }
    }


    public void OpenInventoryWindow(){
        InventoryWin.gameObject.SetActive(true);
        InventoryWin.Init();
    }
    public void CloseInventoryWindow(){
        if (ChoiseWin.gameObject.activeSelf) return;
        InventoryWin.RemoveSlots();
        InventoryWin.gameObject.SetActive(false);
    }
    public void TogleInventoryWindow(){
        if (InventoryWin.gameObject.activeSelf){
            CloseInventoryWindow();
        }else{
            OpenInventoryWindow();
        }
    }


    public void OpenChestWindow(ChestR chest){
        ConectedChest = chest;
        ChestWin.gameObject.SetActive(true);
        ChestWin.Init();
        OpenInventoryWindow();
    }
    public void CloseChestWindow(){
        if (ChoiseWin.gameObject.activeSelf) return;
        ChestWin.RemoveSlots();
        ChestWin.gameObject.SetActive(false);
    }
    public void TogleChestWindow(ChestR chest){
        if (ChestWin.gameObject.activeSelf){
            CloseChestWindow();
        }else{
            OpenChestWindow(chest);
        }
    }


    public void ActiveChoiseWindow(int count, Action<int> callback){   
        if (ChoiseWin.gameObject.activeSelf) return;
        ChoiseWin.Init(count,(num)=>{
            callback(num);
            ChoiseWin.gameObject.SetActive(false);
        });
        ChoiseWin.gameObject.SetActive(true);
    }
    public void CloseChoiseWindow(){
        ChoiseWin.gameObject.SetActive(false);
        ItemSlot.IsMoving = false;
    }
}
