using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMeneger : MonoBehaviour
{
    static GameMeneger Self;

    static public string status = "play";
    int blockGameLavel = 0;

    private void OnEnable() {
        GameMeneger.Self = this;
        GameMeneger.playGame();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.I)){
            Inventory.togleInventory();
        }
    }

    static public void pauseGame(){
        Time.timeScale=0;
        GameMeneger.status = "pause";
        Self.blockGameLavel++;
    }
    static public void playGame(){
        if (Self.blockGameLavel>0){
            Self.blockGameLavel--;
        }

        if (Self.blockGameLavel==0){
            Time.timeScale=1;
            GameMeneger.status = "play";
        }
    }
}
