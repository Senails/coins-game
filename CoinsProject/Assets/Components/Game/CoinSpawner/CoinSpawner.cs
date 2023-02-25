using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject GameMap; 
    void Start()
    {
        float x=0;
        float y=0;

        findPositionForSpawn(ref x, ref y);
        Debug.Log($"{x} {y}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("12312");
    }

    bool checkPosition(float x, float y){
        var check = Physics2D.OverlapCircle(new Vector2(x,y),1);

        if (check==null) return true;
        return false;
    }

    void findPositionForSpawn(ref float x, ref float y){
        x=1;
        y=4;
    }
}
