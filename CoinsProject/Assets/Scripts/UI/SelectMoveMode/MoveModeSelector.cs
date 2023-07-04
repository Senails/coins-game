using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static OptionsManager;

public class MoveModeSelector : MonoBehaviour
{
    static MoveModeSelector self;

    void Start()
    {
        MoveModeSelector.self = this;
        selectFromMemory(); 
    }

    static public void Select(Transform obj){
        for (int i = 0; i < self.transform.childCount; i++){
            var child = self.transform.GetChild(i);

            if (child == obj){
                var sr = child.GetComponent<Image>();
                sr.color = new Color(1, 1, 1, 1);
                self.SetInMemory(i);
            }else{
                var sr = child.GetComponent<Image>();
                sr.color = new Color(1, 1, 1, 0.5f);
            }
        }
    }

    void selectFromMemory(){
        int index = (int) OptionsManager.MoveMode;
        var child = transform.GetChild(index);
        MoveModeSelector.Select(child);
    }

    void SetInMemory(int index){
        OptionsManager.SetMoveMode((MoveModeEnum)index);
    }
}
