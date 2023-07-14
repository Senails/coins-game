using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UiCordsLib;

public class LargeMapWindow : MonoBehaviour
{
    public LargeMapCamera MapCamera;


    private Player _playerObject;


    private void Start() {
        _playerObject = Player.Self;
    }
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
        if (_playerObject==null) Start();
        GameMeneger.PauseGame();
        gameObject.SetActive(true);
        MapCamera.SetPosition(_playerObject.gameObject.transform.position);
    }
}
