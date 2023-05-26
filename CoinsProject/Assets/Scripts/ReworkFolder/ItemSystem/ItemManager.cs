using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{   
    public InventoryR Inventory = new InventoryR();
    public ChestR ConectedChest;


    public InventoryWindow InventoryWin;
    public ChestWindow ChestWin;
    public ChoiseWindow ChoiseWin;
    public static ItemManager Self;


    private void Start() {
        Self = this;
    }
    private void Update() {
        if (Input.GetKey(KeyCode.U)){
            ActiveChoiseWindow(5,(inta)=>{
                Debug.Log(inta);
            });
        }
        if (Input.GetKey(KeyCode.C)){
            if (ChestWin.gameObject.activeSelf) return;
            ChestR chest = new ChestR{

            };

            OpenChestWindow(chest);
        }
    }


    public void OpenInventoryWindow()
    {
        InventoryWin.Init();
        InventoryWin.gameObject.SetActive(true);
    }
    public void CloseInventoryWindow()
    {
        if (ChoiseWin.gameObject.activeSelf) return;
        InventoryWin.gameObject.SetActive(false);
    }
    public void TogleInventoryWindow(){
        if (InventoryWin.gameObject.activeSelf){
            CloseInventoryWindow();
        }else{
            OpenInventoryWindow();
        }
    }


    public void OpenChestWindow(ChestR chest)
    {
        ConectedChest = chest;
        ChestWin.Init();
        ChestWin.gameObject.SetActive(true);
    }
    public void CloseChestWindow()
    {
        if (ChoiseWin.gameObject.activeSelf) return;
        ChestWin.gameObject.SetActive(false);
    }
    public void TogleChestWindow(ChestR chest){
        if (ChestWin.gameObject.activeSelf){
            CloseChestWindow();
        }else{
            OpenChestWindow(chest);
        }
    }


    public void ActiveChoiseWindow(int count, Action<int> callback)
    {   
        if (ChoiseWin.gameObject.activeSelf) return;
        ChoiseWin.Init(count,(num)=>{
            callback(num);
            ChoiseWin.gameObject.SetActive(false);
        });
        ChoiseWin.gameObject.SetActive(true);
    }
}
