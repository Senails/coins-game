using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintScript : MonoBehaviour
{
    float conectObjCount=0;
    static HintScript Self;

    void Start()
    {   
        HintScript.Self=this;
    }

    static public void connect(){
        Self.conectObjCount++;
        Self.ShowHideHint();
    }

    static public void disconnect(){
        Self.conectObjCount--;
        Self.ShowHideHint();
    }

    void ShowHideHint(){
        Image image = transform.GetComponent<Image>();

        if (conectObjCount>0){
            image.color = new Color(1,1,1,1);
        }else{
            image.color = new Color(1,1,1,0);
        }
    }

}
