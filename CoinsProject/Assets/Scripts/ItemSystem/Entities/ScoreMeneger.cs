using System;
using System.Linq;
using UnityEngine;
using TMPro;

using ItemSystemTypes;
using static AsyncLib;
using static ItemSystemUtils;

public class ScoreMeneger : MonoBehaviour
{
    private int _countCoins = 0;
    private static ScoreMeneger _self;
    private Action _cancelAction;


    public TMP_Text ScoreText;
    public GameObject ScoreUi;


    private void Start() {
        _self = this;
        _countCoins=0;
        ScoreText.text = _countCoins.ToString();

        ItemManager.Self.Inventory.OnChange += ()=>{
            _countCoins = RecaculateScore();
            ShowObject();
        };
    }
 

    public int RecaculateScore(){
        int count = 0;

        count+=FindCountItemsInConteiner(ItemManager.Self.Inventory,0);
        count+= 2*FindCountItemsInConteiner(ItemManager.Self.Inventory,1);
        
        return count;
    }

    private void ShowObject(){
        ScoreText.text = _countCoins.ToString();
        _cancelAction?.Invoke();
        ScoreUi.SetActive(true);
        _cancelAction = setTimeout(()=>{
            ScoreUi.SetActive(false);
        },3000);
    }
}