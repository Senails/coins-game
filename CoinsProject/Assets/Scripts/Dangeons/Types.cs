using UnityEngine;


namespace DangeonsTypes{
    [System.Serializable]
    public record DBRoom {
        public int Id = 0;
        public int DoorsCount = 0;
        public GameObject RoomPrefab;
    }


    public record ConnectedRoom{
        public Room room;
        public ConnectedDoor door;
    }




    public enum ConnectedDoor{
        left,
        top,
        right,
        bottom
    }
}