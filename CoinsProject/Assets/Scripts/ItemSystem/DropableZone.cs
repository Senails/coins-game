using System.Collections;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

using static UiCordsLib;

public class DropableZone : MonoBehaviour
{
    private Vector2 _deltaCords;
    private GameObject _clone; 
    private GameObject _origin;
    private Action<float,float> _callback;


    private void Update() {
        if (Input.GetMouseButtonUp(0)){
            pointerUp();
            return;
        }
        pointerMove();
    }
    public void dragStart(GameObject obj,Action<float,float> callback){
        gameObject.SetActive(true);

        _callback=callback;
        createClone(obj);
    }


    public void pointerUp(){
        Destroy(_clone);
        gameObject.SetActive(false);
        try{
            setOpacity(_origin,1f);
        }catch{}

        Vector2 mP = findMousePositionInCanvas();
        _callback(mP.x,mP.y);
    }
    public void pointerMove(){
        Vector2 mP = findMousePositionInCanvas();
        _clone.transform.localPosition = mP + _deltaCords;
    }


    private void createClone(GameObject obj){
        Vector2 mousePos = findMousePositionInCanvas();
        Vector2 objUiCords = findUIPositionInCanvas(obj);
        _deltaCords = objUiCords - mousePos;

        _clone = UnityEngine.Object.Instantiate(obj,transform);
        _origin = obj;

        Vector2 OriginSizes = GetUiSizes(obj);

        RectTransform rectTrClone = _clone.GetComponent<RectTransform>();
        rectTrClone.sizeDelta = new Vector2(OriginSizes.x,OriginSizes.y);

        _clone.transform.localPosition = mousePos+_deltaCords;
        setOpacity(_origin,0.5f);
    }
    private void setOpacity(GameObject obj,float opacity){
        CanvasGroup elem = obj.GetComponent<CanvasGroup>();
        if (elem==null) return;
        elem.alpha = opacity;
    }
}
