using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static AsyncLib;

public class DarkScreen : MonoBehaviour
{
    public int TransitionTime = 1000;
    private CanvasGroup _cg;

    private void Start() {
        _cg = gameObject.GetComponent<CanvasGroup>();
    }


    public void Hide(){
        for(int i=0; i<20;i++){
            int j = i;

            setTimeout(()=>{
                _cg.alpha = (float)1.0 - (j/(float)19.0);
                if (j == 9) gameObject.SetActive(false);
            },i*(TransitionTime/20));
        }
    }
    public void Show(){
        gameObject.SetActive(true);
        for(int i=0; i<20;i++){
            int j = i;

            setTimeout(()=>{
                _cg.alpha = (j/(float)19.0);
            },i*(TransitionTime/20));
        }
    }
}
