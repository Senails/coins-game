using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler
{

    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(1);
    }
}
