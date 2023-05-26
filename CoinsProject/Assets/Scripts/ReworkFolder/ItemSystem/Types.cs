using UnityEngine;


namespace ItemSystemTypes{
    public record ItemR {
        public int id;
        public string name;
        public Sprite itemImage;
        public int mass;
    }


    public record ItemOnInventoryR {
        public Item item = null;
        public int count = 0;
    }


    public enum SlotParent{
        chest,
        inventory,
    }
}