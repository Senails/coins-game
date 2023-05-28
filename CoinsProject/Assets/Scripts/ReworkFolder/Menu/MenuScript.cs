using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void TogleMenu(){
        if (!gameObject.activeSelf){
            gameObject.SetActive(true);
            GameMeneger.PauseGame();
        }else{
            gameObject.SetActive(false);
            GameMeneger.PlayGame();
        }
    }
}
