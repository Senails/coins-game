using System;
using System.Linq;
using UnityEngine;
using TMPro;

using ItemSystemTypes;
using static AsyncLib;

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

        foreach(ItemOnInventoryR elem in ItemManager.Self.Inventory.ItemArray){
            if (elem.item==null || elem.count==0) continue;
            if (elem.item.id==0){
                count+=elem.count;
            }
            if (elem.item.id==1){
                count+=elem.count*2;
            }
        }
        
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