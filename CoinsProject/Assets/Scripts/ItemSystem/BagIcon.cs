using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagIcon : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    double lastClick=0;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (checkLastTime()) return;
        if (Inventory.Self.status == InventoryStatus.hide){
            Inventory.showInventory();
            lastClick = Time.realtimeSinceStartup;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (checkLastTime()) return;
        if (Inventory.Self.status != InventoryStatus.hide){
            Inventory.hideInventory();
            lastClick = Time.realtimeSinceStartup;
        }
    }

    bool checkLastTime(){
        double now = Time.realtimeSinceStartup;
        Debug.Log(lastClick);
        Debug.Log(now);

        if ((now-lastClick)>0.2) return false;
        return true;
    }
}
