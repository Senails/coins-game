using UnityEngine;



namespace ItemSystemTypes{
    [System.Serializable]
    public class ItemR {
        public int id;
        public string name;
        public Sprite itemImage;
        public int mass;
    }


    public class ItemOnInventoryR {
        public ItemR item = null;
        public int count = 0;
    }
    

    public interface ItemListConteiner{
        public ItemOnInventoryR[] ItemArray { get; set; }

        public int HowManyCanAddItem(ItemOnInventoryR item);
        public void AddItem(ItemOnInventoryR item,ItemSlot preferSlot = null);
        public void RemoveItem(ItemOnInventoryR item,ItemSlot preferSlot);
    }
}