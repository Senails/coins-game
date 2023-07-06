using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMeneger : MonoBehaviour
{
    public LargeMapWindow LargeMap;
    public MenuScript Menu;
    public static GameStatus Status = GameStatus.play;
    private static int blockGameLavel = 0;


    private void Start() {
        blockGameLavel = 0;
        GameMeneger.PlayGame();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Menu.TogleMenu();
        }
        if (Input.GetKeyDown(OptionsManager.Config.KyeDictionary["Карта"])){
            LargeMap.TogleMap();
        }
    }


    public static void PauseGame(){
        blockGameLavel++;
        PlayPauseGame();
    }
    public static void PlayGame(){
        blockGameLavel--;
        PlayPauseGame();
    }


    private static void PlayPauseGame(){
        if (blockGameLavel>0){
            Status = GameStatus.pause;
            Time.timeScale=0;
            return;
        }
        if (blockGameLavel<0){
            blockGameLavel = 0;
        }

        Status = GameStatus.play;
        Time.timeScale=1;
    }
    public enum GameStatus{
        play,
        pause
    }
}
