using UnityEngine;
using System.Text.Json;
using UnityEngine.SceneManagement;

using SaveAndLoadingTypes;

public static class GlobalStateSaveMeneger{
    public static int PrevLocationID = 0;


    public static SaveMenegerStatus SaveStatus = SaveMenegerStatus.OnMainMenu;
    

    public static GameSaveConfig SaveConfig = null;
    public static LocationState ActiveLocationState {
        get{
            LocationState state = SaveConfig.LocationSaveList.Find((elem)=>{
                return elem.locationID == SceneManager.GetActiveScene().buildIndex;
            });

            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            Debug.Log(state);

            return state;
        } 
        set{
            LocationState oldState = SaveConfig.LocationSaveList.Find((elem)=>{
                return elem.locationID == SceneManager.GetActiveScene().buildIndex;
            });
            if (oldState!=null){
                SaveConfig.LocationSaveList.Remove(oldState);
            }
            SaveConfig.LocationSaveList.Add(value);
            SaveConfig.ActiveLocationID = SceneManager.GetActiveScene().buildIndex;
        }
    }
}