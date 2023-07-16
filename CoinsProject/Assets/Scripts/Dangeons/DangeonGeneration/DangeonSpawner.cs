using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SaveAndLoadingTypes;
using DangeonsTypes;
using static GameCordsLib;

public class DangeonSpawner : MonoBehaviour
{
    public int RoomsCount = 0;
    public Room RootRoom = null;

    public static int SpawnedCount = 0;
    public static DangeonSpawner Self;


    private static List<Room> AllRoomList = new List<Room>();
    private static Room _rootRoom;


    public void Start(){
        Self = this;
        if (GlobalStateSaveMeneger.SaveStatus == SaveMenegerStatus.LoadingFromSave) return;
        SpawnDangeon();
    }

    public void SpawnDangeon(){
        AllRoomList.Clear();
        SpawnedCount = 0;

        if (RootRoom){
            SpawnedCount++;
            AllRoomList.Add(RootRoom);
            RootRoom.Init(null,RoomsCount-SpawnedCount);
            return;
        }

        List<DBRoom> roomList = RoomsDB.Self.RoomList.FindAll((elem)=>elem.DoorsCount==1);

        System.Random rand = new System.Random();
        int indexRoom = rand.Next(0,roomList.Count-1);

        GameObject prefab = roomList[indexRoom].RoomPrefab;
        _rootRoom = SpawnRoom(prefab,transform.position);
        _rootRoom.Init(null,RoomsCount-SpawnedCount);
    }


    public static Room SpawnRoom(GameObject roomPrefab , Vector2 cords){
        GameObject instance = Instantiate(roomPrefab,new Vector3(cords.x,cords.y,0),new Quaternion());
        SpawnedCount++;
        Room room = instance.GetComponent<Room>();
        AllRoomList.Add(room);
        return room;
    }
    public static void RemoveRoom(Room room){
        foreach(var ChildRoom in room.ListRooms){
            if (ChildRoom==room.Parent){ 
                continue;
            }
            RemoveRoom(ChildRoom.room);
        }

        SpawnedCount--;
        AllRoomList.Remove(room);
        Destroy(room.gameObject);
    }
    public static bool CheckPlaceForRoom(GameObject roomPrefab,Vector2 position,Room parent){
        foreach(Room room in AllRoomList){
            if (room == parent) continue;
            Vector2 roomPos = room.gameObject.transform.position;
            Vector2 delta = position - roomPos;

            Vector2 roomSizes = GetSizesGameObject(room.gameObject);
            Vector2 prefabSizes = GetSizesGameObject(roomPrefab);

            bool checkX = (roomSizes.x/2 + prefabSizes.x/2)<Mathf.Abs(delta.x);
            bool checkY = (roomSizes.y/2 + prefabSizes.y/2)<Mathf.Abs(delta.y);

            if (!checkX && !checkY) return false;
        }
        return true;
    }
}
