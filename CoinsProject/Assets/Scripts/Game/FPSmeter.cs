using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSmeter : MonoBehaviour
{
    int tikCount=0;
    float lastChanges=0;
    float minFPS=5000;

    void Update()
    {   
        lastChanges+=Time.deltaTime;
        tikCount++;
        if (minFPS>1/Time.deltaTime){
            minFPS = Mathf.Floor(1/Time.deltaTime);
        }

        if (lastChanges>0.2){
            renderFPS(Mathf.FloorToInt(tikCount/lastChanges));
            tikCount=0;
            lastChanges=0;
            minFPS=5000;
        }
    }

    void renderFPS(int count){
        TMP_Text tmp = transform.GetComponent<TMP_Text>();
        tmp.text=$"{count}({minFPS}) fps(min)";
    }


}
