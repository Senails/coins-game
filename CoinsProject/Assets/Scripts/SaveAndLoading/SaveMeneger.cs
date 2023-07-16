using System;
using UnityEngine;
using System.Text.Json;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using SaveAndLoadingTypes;

public class SaveMeneger : MonoBehaviour
{   
    public static SaveMeneger Self;
    

    private bool _isInit = false;


    private void Awake() {
        GlobalStateSaveMeneger.SaveConfig = LoadSaveConfig();
        Self = this;
    }
    private void LateUpdate() {
        if (_isInit) return;
        _isInit = true;
        Init();
    }
    private void Init(){
        if (GlobalStateSaveMeneger.SaveStatus == SaveMenegerStatus.OnMainMenu) return;
        if (GlobalStateSaveMeneger.SaveConfig.PlayerSave == null){
            GlobalStateSaveMeneger.SaveConfig.PlayerSave = SavePlayer();
        }else{
            LoadPlayer(GlobalStateSaveMeneger.SaveConfig.PlayerSave);
        }

        if (GlobalStateSaveMeneger.ActiveLocationState == null){
            GlobalStateSaveMeneger.ActiveLocationState = SaveLocation();
        }else{
            LoadLocation(GlobalStateSaveMeneger.ActiveLocationState);
        }

        WriteSaveConfig();
    }
    

    //develop
    private void Update() {
        if (Input.GetKeyDown(KeyCode.U)){
            PlayerPrefs.DeleteAll();
        }
    }
    //develop


    private GameSaveConfig LoadSaveConfig(){
        string textConfig = PlayerPrefs.GetString("GameSaveConfig");
        if (textConfig == "") return new GameSaveConfig();
        GameSaveConfig config = JsonSerializer.Deserialize<GameSaveConfig>(textConfig);
        return config;
    }
    private void WriteSaveConfig(){
        string textConfig = JsonSerializer.Serialize(GlobalStateSaveMeneger.SaveConfig);
        PlayerPrefs.SetString("GameSaveConfig",textConfig);
    }
    private void CreateNewSaveConfig(){
        GlobalStateSaveMeneger.SaveConfig = new GameSaveConfig();
        WriteSaveConfig();
    }


    public void StartNewGame(){
        CreateNewSaveConfig();
        LoadingScene(1);
    }
    public void LoadGame(){
        int index = GlobalStateSaveMeneger.SaveConfig.ActiveLocationID;
        LoadingScene(index);
    }
    public void LoadingScene(int index){
        GameSaveConfig config = GlobalStateSaveMeneger.SaveConfig;
        LocationState locState = config.LocationSaveList.Find((elem)=>{
            return elem.locationID==index;
        });

        if (locState==null){
            GlobalStateSaveMeneger.SaveStatus = SaveMenegerStatus.FreeGeneration;
        }else{
            GlobalStateSaveMeneger.SaveStatus = SaveMenegerStatus.LoadingFromSave;
        }
        if (index == 0){
            GlobalStateSaveMeneger.SaveStatus = SaveMenegerStatus.OnMainMenu;
        }

        GlobalStateSaveMeneger.PrevLocationID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    

    
    public static LocationState SaveLocation(){
        Debug.Log("SaveLocation");
        return new LocationState();
    }
    public static void LoadLocation(LocationState state){
        Debug.Log("LoadLocation");
    }


    public static PlayerState SavePlayer(){
        Debug.Log("SavePlayer");
        return new PlayerState();
    }
    public static void LoadPlayer(PlayerState state){
        Debug.Log("LoadPlayer");
    }



}
