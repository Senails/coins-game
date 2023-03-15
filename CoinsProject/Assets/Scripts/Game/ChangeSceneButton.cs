using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour, IPointerClickHandler
{   
    public int sceneIndex;
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
