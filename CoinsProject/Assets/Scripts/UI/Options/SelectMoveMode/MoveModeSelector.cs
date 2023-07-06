using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using OptionsTypes;

public class MoveModeSelector : MonoBehaviour
{
    public static MoveModeSelector Self;

    public void Start()
    {
        MoveModeSelector.Self = this;
        SelectFromMemory(); 
    }

    static public void Select(Transform obj){
        for (int i = 0; i < Self.transform.childCount; i++){
            var child = Self.transform.GetChild(i);

            if (child == obj){
                var sr = child.GetComponent<Image>();
                sr.color = new Color(1, 1, 1, 1);
                Self.SetInMemory(i);
            }else{
                var sr = child.GetComponent<Image>();
                sr.color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    public void SelectFromMemory(){
        int index = (int) OptionsManager.Config.MoveMode;
        var child = transform.GetChild(index);
        MoveModeSelector.Select(child);
    }

    public void SetInMemory(int index){
        OptionsManager.Self.SetMoveMode((MoveModeEnum)index);
    }
}
