using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;

public class SaveEnemy : MonoBehaviour
{
    public static List<SaveEnemy> EnemyList = new List<SaveEnemy>();


    public void Start(){
        EnemyList.Add(this);
    }
    
    
    private EnemyState GetState(){
        Enemy_Fight enemy = GetComponent<Enemy_Fight>();

        return new EnemyState{
            MaxHealth = enemy.MaxHealth,
            Health = enemy.Health
        };
    }
    private void SetState(EnemyState state){
        Enemy_Fight enemy = GetComponent<Enemy_Fight>();
        enemy.Health = enemy.MaxHealth;
        enemy.RemoveHealth(state.MaxHealth-state.Health);

        if (state.Health == 0){
            gameObject.SetActive(false);
        }
    }


    public static List<EnemyState> SaveEnemys(){
        List<EnemyState> list = new List<EnemyState>();

        foreach(var elem in EnemyList){
            list.Add(elem.GetState());
        }

        return list;
    }
    public static void LoadEnemys(List<EnemyState> list){
        int i = 0;

        foreach(var elem in EnemyList){
            elem.SetState(list[i]);
            i++;
        }
    }
}
