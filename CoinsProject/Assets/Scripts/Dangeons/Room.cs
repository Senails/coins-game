using System.Threading;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using DangeonsTypes;

public class Room : MonoBehaviour
{
    public GameObject LeftDoor = null;
    public GameObject RightDoor = null;
    public GameObject TopDoor = null;
    public GameObject BottomDoor = null;


    public List<ConnectedRoom> ListRooms = new List<ConnectedRoom>();
    public ConnectedRoom Parent = null;


    public bool Init(ConnectedRoom parent,int childrenCount){
        if (parent!=null){
            Parent = parent;
            ListRooms.Add(parent);
        }
        if (DangeonSpawner.Self.RoomsCount==DangeonSpawner.SpawnedCount) return true;
        if (childrenCount==0) return true;

        bool isSucces = SpanwnChildrensRooms(childrenCount);

        return isSucces;
    }


    public bool SpanwnChildrensRooms(int childrenCount){
        List<int> distribute = DistributeRooms(childrenCount);
        int index = 0;

        if (LeftDoor!=null && Parent?.door != ConnectedDoor.right){ 
            bool isSucces = SpanwnOneChildrenRoom(ConnectedDoor.left,distribute[index++]);
            if (!isSucces) return false;
        }
        if (RightDoor!=null && Parent?.door != ConnectedDoor.left){
            bool isSucces = SpanwnOneChildrenRoom(ConnectedDoor.right,distribute[index++]);
            if (!isSucces) return false;
        }
        if (TopDoor!=null && Parent?.door != ConnectedDoor.bottom){
            bool isSucces = SpanwnOneChildrenRoom(ConnectedDoor.top,distribute[index++]);
            if (!isSucces) return false;
        }
        if (BottomDoor!=null && Parent?.door != ConnectedDoor.top) {
            bool isSucces = SpanwnOneChildrenRoom(ConnectedDoor.bottom,distribute[index++]);
            if (!isSucces) return false;
        } 

        return true;
    }
    public bool SpanwnOneChildrenRoom(ConnectedDoor direction,int childrenCount){
        List<GameObject> availableRoomList = FindRoomsForDirection(direction,childrenCount);
        System.Random rand = new System.Random();


        while(true){
            if (availableRoomList.Count == 0) return false;
            int indexRoom = rand.Next(availableRoomList.Count);
            GameObject RoomPrefab = availableRoomList[indexRoom];

            Vector2 newRoomPosition = FindPositionForRoom(RoomPrefab,direction);
            bool isAvailablePlace = DangeonSpawner.CheckPlaceForRoom(RoomPrefab,newRoomPosition,this);

            if (isAvailablePlace){
                Room childRoom = DangeonSpawner.SpawnRoom(RoomPrefab,newRoomPosition);
                bool isSucces = childRoom.Init(new ConnectedRoom{room = this,door = direction} , childrenCount-1);
            
                if (isSucces){
                    ListRooms.Add(new ConnectedRoom{room = childRoom,door = ReverseDirection(direction)});
                    return true;
                }
                DangeonSpawner.RemoveRoom(childRoom);
            }
            availableRoomList.Remove(RoomPrefab);
        }
    }


    private List<int> DistributeRooms(int childrenCount){
        System.Random rand = new System.Random();
        int roomsCount = childrenCount;


        List<int> list = new List<int>();
        int directionCount = FindCountFreeSlots();


        for(int i=0; i<directionCount;i++){
            list.Add(1);
            roomsCount--;
        }
        if (roomsCount==0) return list;


        for(int i=0; i<directionCount;i++){
            if (roomsCount==0) continue;
            int indexRoom = rand.Next(100);
            if (indexRoom>50) continue;
            list[i]+=1;
            roomsCount--;
        }
        if (roomsCount==0) return list;


        int indexDirection = rand.Next(list.Count);
        list[indexDirection]+=roomsCount;

        return list;
    }
    private int FindCountFreeSlots(){
        int count = 0;
        if (LeftDoor != null) count++;
        if (RightDoor != null) count++;
        if (TopDoor != null) count++;
        if (BottomDoor != null) count++;
        if (Parent != null) count--;
        return count;
    }
    
    
    private List<GameObject> FindRoomsForDirection(ConnectedDoor direction,int childrenCount){
        List<DBRoom> availableRoomList = RoomsDB.Self.RoomList.FindAll((elem)=>{
            if (elem.DoorsCount!=1 && childrenCount==1) return false;
            if (elem.DoorsCount!=2 && childrenCount==2) return false;
            if ((elem.DoorsCount==1 || elem.DoorsCount==4) && childrenCount==3) return false;
            if (elem.DoorsCount==1 && childrenCount>3) return false;

            Room room = elem.RoomPrefab.GetComponent<Room>();
            return room.FindDoorRevers(direction) != null;
        });

        return availableRoomList.Select((elem)=>elem.RoomPrefab).ToList();
    }
    private Vector2 FindPositionForRoom(GameObject RoomPrefab,ConnectedDoor direction){
        Room room = RoomPrefab.GetComponent<Room>();
        GameObject Door = room.FindDoorRevers(direction);

        Vector2 deltaCords = RoomPrefab.transform.position - Door.transform.position;
        Vector2 myDoorCords = this.FindDoor(direction).transform.position;

        return myDoorCords+deltaCords;
    }


    private GameObject FindDoorRevers(ConnectedDoor direction){
        if (direction == ConnectedDoor.left) return RightDoor;
        if (direction == ConnectedDoor.right) return LeftDoor;
        if (direction == ConnectedDoor.top) return BottomDoor;
        if (direction == ConnectedDoor.bottom) return TopDoor;
        return null;
    }
    private GameObject FindDoor(ConnectedDoor direction){
        if (direction == ConnectedDoor.left) return LeftDoor;
        if (direction == ConnectedDoor.right) return RightDoor;
        if (direction == ConnectedDoor.top) return TopDoor;
        if (direction == ConnectedDoor.bottom) return BottomDoor;
        return null;
    }
    private ConnectedDoor ReverseDirection(ConnectedDoor direction){
        if (direction == ConnectedDoor.left) return ConnectedDoor.right;
        if (direction == ConnectedDoor.right) return ConnectedDoor.left;
        if (direction == ConnectedDoor.top) return ConnectedDoor.bottom;
        return ConnectedDoor.top;
    }
}
