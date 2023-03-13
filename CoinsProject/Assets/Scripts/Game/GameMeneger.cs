using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMeneger : MonoBehaviour
{
    private void Start() {
        playGame();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.I)){
            Inventory.togleInventory();
        }
    }

    static public void pauseGame(){
        Time.timeScale=0;
    }
    static public void playGame(){
        Time.timeScale=1;
    }
}
