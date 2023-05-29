using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerClickHandler
{   
    public GameObject PauseMenu;
    private static bool IsHide = false;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsHide){
            PauseMenu.gameObject.SetActive(true);
            GameMeneger.PauseGame();
        }else{
            PauseMenu.gameObject.SetActive(false);
            GameMeneger.PlayGame();
        }
        IsHide = !IsHide;
    }


}
