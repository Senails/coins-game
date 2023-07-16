using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SaveAndLoadingTypes;

public class SaveEnemy : MonoBehaviour
{
    public static List<SaveEnemy> EnemyList = new List<SaveEnemy>();

    private void Awake(){
        EnemyList.Clear();
    }
    private void Start(){
        EnemyList.Add(this);
    }

    public static List<EnemyState> SaveEnemys(){
        return new List<EnemyState>();
    }
    public static void LoadEnemys(List<EnemyState> list){

    }
}
