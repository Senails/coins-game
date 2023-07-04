using System.Collections;
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


    private List<ConnectedRoom> ListRooms = new List<ConnectedRoom>();
    private ConnectedRoom _parent = null;


    public bool Init(ConnectedRoom parent,int childrenCount){
        if (parent!=null){
            _parent = parent;
            ListRooms.Add(parent);
        }
        if (DangeonSpawner.Self.RoomsCount==DangeonSpawner.SpawnedCount) return true;
        if (childrenCount==0) return true;


        bool isSucces = SpanwnChildrensRooms(childrenCount);
        return isSucces;
    }


    public bool SpanwnChildrensRooms(int childrenCount){
        // Debug.Log(DangeonSpawner.Self.RoomsCount);
        // Debug.Log(DangeonSpawner.SpawnedCount);

        if (LeftDoor!=null && _parent?.door != ConnectedDoor.right){ 
            return SpanwnOneChildrenRoom(ConnectedDoor.left,childrenCount);
        }
        if (RightDoor!=null && _parent?.door != ConnectedDoor.left){
            return SpanwnOneChildrenRoom(ConnectedDoor.right,childrenCount);
        }
        if (TopDoor!=null && _parent?.door != ConnectedDoor.bottom){
            return SpanwnOneChildrenRoom(ConnectedDoor.top,childrenCount);
        }
        if (BottomDoor!=null && _parent?.door != ConnectedDoor.top) {
            return SpanwnOneChildrenRoom(ConnectedDoor.bottom,childrenCount);
        } 

        return true;
    }
    public bool SpanwnOneChildrenRoom(ConnectedDoor direction,int childrenCount){
        List<GameObject> availableRoomList = FindRoomsForDirection(direction,childrenCount);

        System.Random rand = new System.Random();
        int indexRoom = rand.Next(availableRoomList.Count);

        GameObject RoomPrefab = availableRoomList[indexRoom];
        Vector2 newRoomPosition = FindPositionForRoom(RoomPrefab,direction);
        bool isAvailablePlace = DangeonSpawner.CheckPlaceForRoom(RoomPrefab,newRoomPosition,this);

        Debug.Log(isAvailablePlace);

        Room childRoom = DangeonSpawner.SpawnRoom(RoomPrefab,newRoomPosition);
        return childRoom.Init(new ConnectedRoom{room = this,door = direction} , childrenCount-1);
    }


    private int FindCountFreeSlots(){
        int count = 0;
        if (LeftDoor != null) count++;
        if (RightDoor != null) count++;
        if (TopDoor != null) count++;
        if (BottomDoor != null) count++;
        if (_parent != null) count--;
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
}
