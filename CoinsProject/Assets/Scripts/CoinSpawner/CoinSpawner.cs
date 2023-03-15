using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject GameMap; 
    public GameObject SmallCoinPrefab;
    public GameObject LargeCoinPrefab;

    float minX;
    float maxX;
    float minY;
    float maxY;
    void Start()
    {
        spawnCoins();
    }

    bool checkPosition(float x, float y){
        var check = Physics2D.OverlapCircle(new Vector2(x,y),1);

        if (check==null) return true;
        return false;
    }

    void findPositionForSpawn(ref float x, ref float y){
        while (true){
            float randX = Random.Range(minX,maxX);
            float randY = Random.Range(minY,maxY);

            if (checkPosition(randX,randY)){
                x=randX;
                y=randY;
                break;
            }
        }
    }

    void findMinMaxPosition(){
        float mapWidthPart = GameMap.transform.localScale.x/2;
        float mapHeightPart = GameMap.transform.localScale.y/2;

        float mapX = GameMap.transform.position.x;
        float mapY = GameMap.transform.position.y;

        this.minX = mapX-mapWidthPart;
        this.maxX = mapX+mapWidthPart;
        this.minY = mapY-mapHeightPart;
        this.maxY = mapY+mapHeightPart;
    }

    void spawnCoins(){
        findMinMaxPosition();

        float x=0;
        float y=0;

        for(int i=0;i<50;i++){
            findPositionForSpawn(ref x,ref y);
            spawnOneCoin(x,y,"small");
        }
        for(int i=0;i<50;i++){
            findPositionForSpawn(ref x,ref y);
            spawnOneCoin(x,y,"large");
        }
    }

    void spawnOneCoin(float x , float y, string size){
        if (size=="small"){
            Instantiate(SmallCoinPrefab,new Vector2(x,y),new Quaternion());
        }else{
            Instantiate(LargeCoinPrefab,new Vector2(x,y),new Quaternion());
        }
    }
}
