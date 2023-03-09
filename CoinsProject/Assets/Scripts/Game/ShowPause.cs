using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShowPause : MonoBehaviour, IPointerClickHandler
{   
    public GameObject PauseMenu;
    public string ShowHide = "show";
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ShowHide=="show"){
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale=0;
        }else{
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale=1;
        }
    }
}
