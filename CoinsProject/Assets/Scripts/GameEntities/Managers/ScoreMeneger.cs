using System;
using UnityEngine;
using TMPro;

using static AsyncLib;

public class ScoreMeneger : MonoBehaviour
{
    private static int _countCoins = 0;
    private static ScoreMeneger _self;
    private static Action _cancelAction;


    public TMP_Text ScoreText;
    public GameObject ScoreUi;


    private void Start() {
        _self = this;
        _countCoins=0;
        ScoreText.text = _countCoins.ToString();

        // ItemManager.Self.Inventory.OnChange +=
    }


    public static void AddCoins(int count){
        _countCoins+=count;
        ShowObject();
    }
    public static void RemoveCoins(int count){
        _countCoins-=count;
        ShowObject();
    }


    private static void ShowObject(){
        _self.ScoreText.text = _countCoins.ToString();
        _cancelAction?.Invoke();
        _self.ScoreUi.SetActive(true);
        _cancelAction = setTimeout(()=>{
            _self.ScoreUi.SetActive(false);
        },3000);
    }
}