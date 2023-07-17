using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;

public class SaveStaticConteiner : MonoBehaviour{
    public static List<SaveStaticConteiner> ConteinersList = new List<SaveStaticConteiner>();

    
    private void Start(){
        ConteinersList.Add(this);
    }
    private void OnDestroy() {
        ConteinersList.Remove(this);
    }


    public List<ItemData> GetItemList(){
        ChestSript script = GetComponent<ChestSript>();
        ChestR chest = script.ChestEntiti;
        return chest.GetSaveList();
    }
    public void LoadItemList(List<ItemData> list){
        ChestSript script = GetComponent<ChestSript>();
        ChestR chest = script.ChestEntiti;
        chest.LoadSaveList(list);
    }


    public static List<StaticConteinerState> SaveStaticConteiners(){
        List<StaticConteinerState> list = new List<StaticConteinerState>();

        foreach(var elem in ConteinersList){
            list.Add(new StaticConteinerState{
                ConteinerItemsSave = elem.GetItemList()
            });
        }

        return list;
    }
    public static void LoadStaticConteiners(List<StaticConteinerState> list){
        int i = 0;
        foreach(var elem in ConteinersList){
            elem.LoadItemList(list[i].ConteinerItemsSave);
            i++;
        }
    }
}
