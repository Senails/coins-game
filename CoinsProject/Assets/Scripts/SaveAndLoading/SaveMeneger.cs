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
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
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
    private void SaveDateToConfig(){
        if (SceneManager.GetActiveScene().buildIndex == 0) return;
        GlobalStateSaveMeneger.SaveConfig.PlayerSave = SavePlayer();
        GlobalStateSaveMeneger.ActiveLocationState = SaveLocation();
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

        SaveDateToConfig();

        if (index == 0){
            GlobalStateSaveMeneger.SaveStatus = SaveMenegerStatus.OnMainMenu;
        }

        GlobalStateSaveMeneger.PrevLocationID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    
    public static PlayerState SavePlayer(){
        PlayerState playerState = new PlayerState{
            IsDeath = Player.Self.IsDeath,
            MaxHealth = Player.Self.MaxHealth,
            Health = Player.Self.Health
        };

        playerState.InvetoryItemsSave = InventoryR.Self.GetSaveList();
        playerState.ItemPanelSave = ItemPanel.Self.GetSaveList();

        return playerState;
    }
    public static void LoadPlayer(PlayerState state){
        Player.Self.Health = state.MaxHealth;
        Player.Self.RemoveHealth(state.MaxHealth-state.Health);

        InventoryR.Self.LoadSaveList(state.InvetoryItemsSave);
        ItemPanel.Self.LoadSaveList(state.ItemPanelSave);
    }

    
    public static LocationState SaveLocation(){
        LocationState state = new LocationState{
            locationID = SceneManager.GetActiveScene().buildIndex,
            PlayerPositionX = Player.Self.transform.position.x,
            PlayerPositionY = Player.Self.transform.position.y,
        };

        state.ConteinersOnLocation = SaveStaticConteiner.SaveStaticConteiners();
        state.EnemysOnLocation = SaveEnemy.SaveEnemys();
        state.PrefabsOnLocation = SavePrefab.SavePrefabs();

        Debug.Log("SaveLocation "+$"{SceneManager.GetActiveScene().buildIndex}");
    
        return state;
    }
    public static void LoadLocation(LocationState state){
        Player.Self.transform.position = 
        new Vector2(state.PlayerPositionX,state.PlayerPositionY);

        SaveStaticConteiner.LoadStaticConteiners(state.ConteinersOnLocation);
        SaveEnemy.LoadEnemys(state.EnemysOnLocation);
        SavePrefab.LoadPrefabs(state.PrefabsOnLocation);

        Debug.Log("LoadLocation");
    }

}
