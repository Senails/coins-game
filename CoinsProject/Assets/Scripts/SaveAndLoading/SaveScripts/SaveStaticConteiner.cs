using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;

public class SaveStaticConteiner : MonoBehaviour{
    public static List<SaveStaticConteiner> ConteinersList = new List<SaveStaticConteiner>();

    private void Awake(){
        ConteinersList.Clear();
    }
    private void Start(){
        ConteinersList.Add(this);
    }

    public static List<StaticConteinerState> SaveStaticConteiners(){
        return new List<StaticConteinerState>();
    }
    public static void LoadStaticConteiners(List<StaticConteinerState> list){

    }
}
