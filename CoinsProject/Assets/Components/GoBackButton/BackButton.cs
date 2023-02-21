using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour, IPointerClickHandler
{
    public float posTop = 0;
    public float posButtom = 0;
    public float posLeft = 0;
    public float posRight = 0;

    void Start()
    {
        placeOnStart();
    }

    void placeOnStart(){
        if (posRight != 0){
            Utils.setPositionRight(transform, posRight);
        }else{
            Utils.setPositionLeft(transform, posLeft);
        }

        if (posButtom != 0){
            Utils.setPositionBottom(transform, posButtom);
        }else{
            Utils.setPositionTop(transform, posTop);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(1);
    }
}
