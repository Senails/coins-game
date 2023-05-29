using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static OptionsManager;

public class OptionsManager : MonoBehaviour
{
    public static MoveModeEnum MoveMode = MoveModeEnum.keyboard;
    
    public void Start()
    {
        if (PlayerPrefs.HasKey("moveMode")){
            MoveMode = (MoveModeEnum) PlayerPrefs.GetInt("MoveMode");
        }
    }

    public static void SetMoveMode(MoveModeEnum mode){
        MoveMode = mode;
        PlayerPrefs.SetInt("MoveMode",(int)mode);
    }

    public enum MoveModeEnum{
        keyboard,
        mouse,
        hybrid,
    }
}
