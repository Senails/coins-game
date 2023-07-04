using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UiCordsLib;

public class LargeMapWindow : MonoBehaviour
{
    public LargeMapCamera MapCamera;
    public GameObject Player;


    public void Close(){
        gameObject.SetActive(false);
    }
    public void Open(){
        gameObject.SetActive(true);
        MapCamera.SetPosition(Player.transform.position);
    }
}
