using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMeneger : MonoBehaviour
{
    public static GameStatus Status = GameStatus.play;
    private static int blockGameLavel = 0;


    private void Start() {
        blockGameLavel = 0;
        GameMeneger.PlayGame();
    }


    static public void PauseGame(){
        blockGameLavel++;
        PlayPauseGame();
    }
    static public void PlayGame(){
        blockGameLavel--;
        PlayPauseGame();
    }


    static private void PlayPauseGame(){
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
