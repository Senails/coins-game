using UnityEngine;
using ItemSystemTypes;


public class InventoryWindow : MonoBehaviour
{   
    public GameObject SlotConteiner;


    public void Init(){
        ItemManager.Self.Inventory.OnChange = RenderSlots;
        RenderSlots();
    }
    public void CancelInventoryWindow(){
        ItemManager.Self.CloseInventoryWindow();
    }
 

    private void RenderSlots(){
        if (!gameObject.activeSelf) return;
        RemoveSlots();
        ItemOnInventoryR[] arr = ItemManager.Self.Inventory.ItemArray;

        foreach(var Item in arr){
            GameObject slot = Object.Instantiate(ItemManager.Self.SlotPrefab,SlotConteiner.transform);
            ItemSlot itemSlot = slot.GetComponent<ItemSlot>();
            itemSlot.Init(Item,ItemManager.Self.Inventory);
        }
    }
    public void RemoveSlots(){
        foreach(var slot in ItemSlot.ListAllSlotsOnScreen.ToArray()){
            if (slot.Parent==ItemManager.Self.Inventory) slot.Remove();
        }
    }
}