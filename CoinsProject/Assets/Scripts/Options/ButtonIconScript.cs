using System;
using UnityEngine;
using TMPro;
using ItemSystemTypes;

using static KeyUtils;


public class ButtonIconScript : MonoBehaviour
{
    public string KeyName;
    public TMP_Text KeyText;


    private void Start() {
        OptionsManager.Self.OnChangeConfig += Render;
        Render();
    }

    private void Render(){
        KeyCode code = OptionsManager.Config.KyeDictionary[KeyName];
        KeyText.text = GetNameKey(code);
    }
}