using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    private static float _conectObjCount;
    private static HintManager _self;
    public GameObject HintIcon;


    private void Start() {
        _conectObjCount = 0;
        _self = this;
    }

    public static void Connect(){
        _conectObjCount++;
        ShowHideHint();
    }

    public static void Disconnect(){
        _conectObjCount--;
        ShowHideHint();
    }

    public static void ShowHideHint(){
        if (_conectObjCount>0){
            _self.HintIcon.SetActive(true);
        }else{
            _self.HintIcon.SetActive(false);
        }
    }
}
