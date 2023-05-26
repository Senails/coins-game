using UnityEngine;
using ItemSystemTypes;


public class InventoryWindow : MonoBehaviour
{   
    public GameObject SlotConteiner;
    public GameObject SlotPrefab;


    public void Init(){
        ItemManager.Self.Inventory.OnChange = RenderSlots;
        RenderSlots();
    }
    public void CancelInventoryWindow(){
        ItemManager.Self.CloseInventoryWindow();
    }
 

    private void RenderSlots(){
        RemoveSlots();
        ItemOnInventoryR[] arr = ItemManager.Self.Inventory.ItemArray;

        foreach(var Item in arr){
            GameObject slot = Object.Instantiate(SlotPrefab,SlotConteiner.transform);
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            itemSlot.Init(Item,SlotParent.inventory);
        }
    }
    private void RemoveSlots(){
        foreach(var slot in ItemSlot.ListAllSlotsOnScreen.ToArray()){
            if (slot.Parent==SlotParent.inventory) slot.Remove();
        }
    }
}