using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiseWindow : MonoBehaviour
{
    public Slider Slider;
    public TMP_Text TextBlock;


    private Action<int> _onChoiseCallback;


    private void Update() {
        int maxItemCount = (int)Mathf.Floor(Slider.maxValue);
        int activeCount = (int)Mathf.Floor(Slider.value);

        render((int)Mathf.Floor(Slider.value),(int)Mathf.Floor(Slider.maxValue));
    }
    
    
    public void Init(int count, Action<int> callback){
        Slider.maxValue=count;
        Slider.value=0;
        _onChoiseCallback = callback;
    }
    public void setChoise(){
        _onChoiseCallback?.Invoke((int)Mathf.Floor(Slider.value));
    }
    public void cancelChoiseWindow(){
        _onChoiseCallback?.Invoke(0);
    }


    private void render(int activeCount, int maxItemCount){
        TextBlock.text = $"{activeCount} / {maxItemCount}";
    }
}
