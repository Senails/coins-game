using System;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;

public class SavePrefab : MonoBehaviour
{
    public int PrefabID;


    public static List<SavePrefab> PrefabList = new List<SavePrefab>();


    private void Awake(){
        PrefabList.Clear();
    }
    private void Start(){
        PrefabList.Add(this);
    }

    public static List<PrefabState> SavePrefabs(){
        return new List<PrefabState>();
    }
    public static void LoadPrefabs(List<PrefabState> list){

    }
}
