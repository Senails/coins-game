using System;
using UnityEngine;
using System.Text.Json;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using SaveAndLoadingTypes;
using static AsyncLib;

public class SaveMeneger : MonoBehaviour
{   
    public static SaveMeneger Self;
    public DarkScreen DarkScrn;


    private bool _isInit = false;


    private void Awake() {
        GlobalStateSaveMeneger.SaveConfig = LoadSaveConfig();
        Self = this;

        SaveEnemy.EnemyList.Clear();
        SavePrefab.PrefabList.Clear();
        SaveStaticConteiner.ConteinersList.Clear();
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

        GameMeneger.PauseGame();
        DarkScrn.Hide();
        setTimeout(()=>{
            GameMeneger.PlayGame();
        },DarkScrn.TransitionTime);
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
        if (DarkScrn!=null){
            GameMeneger.PauseGame();
            DarkScrn.Show();
            setTimeout(()=>{
                ChangeScene(index);
            },DarkScrn.TransitionTime);
        }else{
            ChangeScene(index);
        }
    }
    private void ChangeScene(int index){
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
            Health = Player.Self.Health,
            CameraSize = Camera.main.orthographicSize/CameraController.Self.NormalCameraSize
        };


        playerState.InvetoryItemsSave = InventoryR.Self.GetSaveList();
        playerState.ItemPanelSave = ItemPanel.Self.GetSaveList();

        return playerState;
    }
    public static void LoadPlayer(PlayerState state){
        Player.Self.Health = state.MaxHealth;
        Player.Self.RemoveHealth(state.MaxHealth-state.Health);

        Camera.main.orthographicSize = CameraController.Self.NormalCameraSize*state.CameraSize;

        InventoryR.Self.LoadSaveList(state.InvetoryItemsSave);
        ItemPanel.Self.LoadSaveList(state.ItemPanelSave);
    }

    
    public static LocationState SaveLocation(){
        LocationState state = new LocationState{
            locationID = SceneManager.GetActiveScene().buildIndex,
            PlayerPositionX = Player.Self.transform.position.x,
            PlayerPositionY = Player.Self.transform.position.y,
        };
        Player.Self.StartPosition = Player.Self.transform.position;

        state.PrefabsOnLocation = SavePrefab.SavePrefabs();
        state.ConteinersOnLocation = SaveStaticConteiner.SaveStaticConteiners();
        state.EnemysOnLocation = SaveEnemy.SaveEnemys();

        return state;
    }
    public static void LoadLocation(LocationState state){
        Player.Self.transform.position = 
        new Vector2(state.PlayerPositionX,state.PlayerPositionY);

        Player.Self.StartPosition = Player.Self.transform.position;

        SavePrefab.LoadPrefabs(state.PrefabsOnLocation);
        setTimeout(()=>{
            SaveStaticConteiner.LoadStaticConteiners(state.ConteinersOnLocation);
            SaveEnemy.LoadEnemys(state.EnemysOnLocation);
        },1);
    }

}
