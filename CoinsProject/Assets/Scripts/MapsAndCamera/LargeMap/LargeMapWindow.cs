using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UiCordsLib;

public class LargeMapWindow : MonoBehaviour
{
    public LargeMapCamera MapCamera;
    public GameObject Player;

    public void TogleMap(){
        if (gameObject.activeSelf){
            Close();
        }else{
            Open();
        }
    }
    public void Close(){
        GameMeneger.PlayGame();
        gameObject.SetActive(false);
    }
    public void Open(){
        GameMeneger.PauseGame();
        gameObject.SetActive(true);
        MapCamera.SetPosition(Player.transform.position);
    }
}
