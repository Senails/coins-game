using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;
using static GameCordsLib;

public class SpawZone : MonoBehaviour
{
    public GameObject PrefubForSpawn;
    public int Count = 0;
    public float MinGap { get; init; } = 0.1f;

    public void Start(){
        if (GlobalStateSaveMeneger.SaveStatus == SaveMenegerStatus.LoadingFromSave) return;
        Spawn();
    }

    private void Spawn(){
        for(int i = 0; i<Count; i++){
            try{
                Vector2 cords = FindFreeCordsInGameRect(this.gameObject,MinGap);
                Instantiate(PrefubForSpawn,cords,new Quaternion());
            }catch{
                Debug.Log("ошибка при спавне");
            }
        }
    }
}
