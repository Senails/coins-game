using System;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;
namespace ItemSystemTypes{
    [Serializable]
    public class ItemR {
        public int id;
        public string name;
        public Sprite itemImage;
        public int mass;
        public ActionBase OnUseAction;
    }


    public class ItemOnInventoryR {
        public ItemR item = null;
        public int count = 0;
    }
    

    public interface ItemListConteiner{
        public ItemOnInventoryR[] ItemArray { get; set; }
        public bool CanDrop { get; set; }
        public bool CanPlace { get; set; }
        public bool CanTake { get; set; }


        public int HowManyCanAddItem(ItemOnInventoryR item);
        public void AddItem(ItemOnInventoryR item,ItemSlot preferSlot = null);
        public void RemoveItem(ItemOnInventoryR item,ItemSlot preferSlot);
        public void Render();


        public List<ItemData> GetSaveList();
        public void LoadSaveList(List<ItemData> list);


    }

    public abstract class ActionBase : MonoBehaviour
    {
        public abstract void Invoke();
    }
}