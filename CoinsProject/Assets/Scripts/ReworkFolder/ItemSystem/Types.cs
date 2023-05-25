using UnityEngine;


namespace ItemSystemTypes{
    public record ItemR {
        public int id;
        public string name;
        public Sprite itemImage;
        public int mass;
    }


    public record ItemOnInventoryR {
        public Item item;
        public int count;
    }
}