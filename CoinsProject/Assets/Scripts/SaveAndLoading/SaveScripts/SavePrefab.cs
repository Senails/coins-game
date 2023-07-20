using System;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;

public class SavePrefab : MonoBehaviour
{
    public int PrefabID;


    public static List<SavePrefab> PrefabList = new List<SavePrefab>();


    private void Start(){
        PrefabList.Add(this);
    }
    private void OnDestroy() {
        PrefabList.Remove(this);
    }


    public static List<PrefabState> SavePrefabs(){
        List<PrefabState> list = new List<PrefabState>();
        foreach(var elem in PrefabList){
            list.Add(new PrefabState{
                PrefabID = elem.PrefabID,
                PrefabPositionX = elem.transform.position.x,
                PrefabPositionY = elem.transform.position.y
            });
        }

        return list;
    }
    public static void LoadPrefabs(List<PrefabState> list){
        foreach(PrefabState elem in list){
            Vector2 position = new Vector2(elem.PrefabPositionX,elem.PrefabPositionY);
            GameObject prefab = PrefabDB.Self.PrefabListDB[elem.PrefabID];
            Instantiate(prefab,position,new Quaternion());
        }
    }
}
