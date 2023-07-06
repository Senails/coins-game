using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;


using OptionsTypes;
using static KeyUtils;

public class OptionsManager : MonoBehaviour
{
    public static OptionsConfig Config;
    public static OptionsManager Self;

    public event Action OnChangeConfig;


    public void Awake() {
        Self = this;
        Config = LoadOptionsConfig();
    }


    private void SaveOptionsConfig(){
        string textConfig = JsonSerializer.Serialize(Config);
        PlayerPrefs.SetString("options",textConfig);
        OnChangeConfig?.Invoke();
    }
    private OptionsConfig LoadOptionsConfig(){
        string textConfig = PlayerPrefs.GetString("options");
        if (textConfig == "") return new OptionsConfig();
        OptionsConfig config = JsonSerializer.Deserialize<OptionsConfig>(textConfig);
        return config;
    }


    public void SetActionKey(string actionName, KeyCode code){
        if (!Config.KyeDictionary.ContainsKey(actionName)) return;
        if (Config.KyeDictionary.ContainsValue(code)) return;

        Config.KyeDictionary[actionName] = code;
        SaveOptionsConfig();
    }
    public void SetMoveMode(MoveModeEnum mode){
        Config.MoveMode = mode;
        SaveOptionsConfig();
    }
    public void ResetOptions(){
        Config = new OptionsConfig();
        SaveOptionsConfig();
    }
}
