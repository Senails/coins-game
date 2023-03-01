using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSmeter : MonoBehaviour
{
    int tikCount=0;
    float lastChanges=0;

    void Start()
    {
    }

    void Update()
    {   
        lastChanges+=Time.deltaTime;
        tikCount++;
        if (lastChanges>0.2){
            renderFPS(Mathf.FloorToInt(tikCount*5));
            tikCount=0;
            lastChanges=0;
        }
    }

    void renderFPS(int count){
        TMP_Text tmp = transform.GetComponent<TMP_Text>();
        tmp.text=$"{count} fps";
    }


}
