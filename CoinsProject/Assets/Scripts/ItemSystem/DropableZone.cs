using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableZone : MonoBehaviour
{
    static public DropableZone Self;
    float dX = 0;
    float dY = 0;
    bool droping = false;
    GameObject clone; 
    GameObject origin;

    

    private void Start() {
        DropableZone.Self = this;
        this.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)){
            pointerUp();
            return;
        }

        pointerMove();
    }

    static public void dragStart(GameObject obj){
        Self.gameObject.SetActive(true);
        Self.droping = true;

        Self.createClone(obj);
    }

    public void pointerUp()
    {   
        droping = false;
        Destroy(clone);
        dX=0;
        dY=0;
        gameObject.SetActive(false);
        setOpacity(origin,1f);


    }

    public void pointerMove(){ 
        if (!droping) return;

        float x = 0;
        float y = 0;
        findMousePosition(ref x,ref y);

        clone.transform.localPosition = new Vector3(x+dX,y+dY);
    }

    void findMousePosition(ref float x,ref float y){
        float mouseDX = Input.mousePosition.x/Screen.width-0.5f;
        float mouseDY = Input.mousePosition.y/Screen.height-0.5f;

        RectTransform rectTr = transform.GetComponent<RectTransform>();
        Rect rect = rectTr.rect;

        float width = rect.width;
        float height = rect.height;

        x = width*mouseDX+this.dX;
        y = height*mouseDY+this.dY;
    }

    void createClone(GameObject obj){
        float x = 0;
        float y = 0;
        findMousePosition(ref x,ref y);

        clone = Object.Instantiate(obj,transform);
        origin = obj;

        RectTransform rectTrClone = clone.GetComponent<RectTransform>();
        Rect rectOrigin = obj.GetComponent<RectTransform>().rect;

        rectTrClone.sizeDelta = new Vector2(rectOrigin.width,rectOrigin.height);

        clone.transform.localPosition = 
        new Vector3(x+dX,y+dY);

        setOpacity(origin,0.5f);
    }

    void setOpacity(GameObject obj,float opacity){
        CanvasGroup elem = obj.GetComponent<CanvasGroup>();
        if (elem==null) return;

        elem.alpha = opacity;
    }

}
