using System.Collections.Generic;


namespace SaveAndLoadingTypes{

    public record GameSaveConfig{
        public int ActiveLocationID {get; set;} = 0;


        public PlayerState PlayerSave {get; set;} = null;
        public List<LocationState> LocationSaveList {get; set;} = new List<LocationState>();
    }


    public record PlayerState{
        public bool IsDeath {get; set;} = false;
        public int MaxHealth {get; set;} = 100;
        public int Health {get; set;} = 100;
        public float CameraSize {get; set;} = 1;


        public List<ItemData> InvetoryItemsSave {get; set;} = new List<ItemData>();
        public List<ItemData> ItemPanelSave {get; set;} = new List<ItemData>();
    }
    public record EnemyState{
        public int MaxHealth {get; set;} = 20;
        public int Health {get; set;} = 20;
    }
    public record StaticConteinerState{
        public List<ItemData> ConteinerItemsSave {get; set;} = new List<ItemData>();
    }
    public record PrefabState{
        public int PrefabID {get; set;} = 0;


        public float PrefabPositionX {get; set;} = 0;
        public float PrefabPositionY {get; set;} = 0;
    }


    public class LocationState{
        public int locationID {get; set;} = 1;


        public float PlayerPositionX {get; set;} = 0;
        public float PlayerPositionY {get; set;} = 0;


        public List<PrefabState> PrefabsOnLocation {get; set;} = new List<PrefabState>();
        public List<EnemyState> EnemysOnLocation {get; set;} = new List<EnemyState>();
        public List<StaticConteinerState> ConteinersOnLocation {get; set;} = new List<StaticConteinerState>();
    }


    public record ItemData{
        public int itemID {get; set;} = 0;
        public int count {get; set;} = 0;
    }
    public enum SaveMenegerStatus{
        OnMainMenu,
        LoadingFromSave,
        FreeGeneration,
    }
}
