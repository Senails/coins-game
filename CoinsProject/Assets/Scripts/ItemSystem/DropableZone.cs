using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableZone : MonoBehaviour
{
    static public DropableZone Self;
    float dX = 0;
    float dY = 0;
    public bool droping = false;
    GameObject clone; 
    GameObject origin;

    public onDragEnd callback;

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

    static public void dragStart(GameObject obj,onDragEnd callback){
        Self.gameObject.SetActive(true);
        Self.droping = true;

        Self.callback=callback;
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


        float mouseX = 0;
        float mouseY = 0;
        
        findMousePositionAboutCanvas(ref mouseX,ref mouseY);
        callback(mouseX,mouseY);
    }

    public void pointerMove(){ 
        if (!droping) return;

        float x = 0;
        float y = 0;
        findMousePositionAboutCanvas(ref x,ref y);

        clone.transform.localPosition = new Vector3(x+dX,y+dY);
    }

    static public void findMousePositionAboutCanvas(ref float x,ref float y){
        float mouseDX = Input.mousePosition.x/Screen.width-0.5f;
        float mouseDY = Input.mousePosition.y/Screen.height-0.5f;

        RectTransform rectTr = Self.transform.GetComponent<RectTransform>();
        Rect rect = rectTr.rect;

        float width = rect.width;
        float height = rect.height;

        x = width*mouseDX;
        y = height*mouseDY;
    }

    static public void findUIPositionAboutCanvas(ref float x,ref float y,GameObject obj){
        Camera camera = Camera.main;

        float globalCameraX = camera.transform.position.x;
        float globalCameraY = camera.transform.position.y;

        float globalObjectX = obj.transform.position.x;
        float globalObjectY = obj.transform.position.y;

        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        float deltaX = (globalObjectX - globalCameraX)/widthCamera;
        float deltaY = (globalObjectY - globalCameraY)/heightCamera;

        RectTransform rectTr = Self.transform.GetComponent<RectTransform>();
        Rect rect = rectTr.rect;

        float width = rect.width;
        float height = rect.height;

        x = width*deltaX;
        y = height*deltaY;
    }

    static public bool checkDropOnRect( float x, float y,GameObject obj){
        float objX = 0;
        float objY = 0;
        findUIPositionAboutCanvas(ref objX,ref objY,obj);

        Rect rect = obj.GetComponent<RectTransform>().rect;

        float objWidth = rect.width;
        float objHeight = rect.height;

        if (Mathf.Abs(objX-x)>objWidth/2) return false;
        if (Mathf.Abs(objY-y)>objHeight/2) return false;

        return true;
    }

    void createClone(GameObject obj){
        float mouseX = 0;
        float mouseY = 0;
        findMousePositionAboutCanvas(ref mouseX,ref mouseY);

        float objX = 0;
        float objY = 0;
        findUIPositionAboutCanvas(ref objX,ref objY,obj);
      
        this.dX=objX-mouseX;
        this.dY=objY-mouseY;

        clone = Object.Instantiate(obj,transform);
        origin = obj;

        RectTransform rectTrClone = clone.GetComponent<RectTransform>();
        Rect rectOrigin = obj.GetComponent<RectTransform>().rect;

        rectTrClone.sizeDelta = new Vector2(rectOrigin.width,rectOrigin.height);

        clone.transform.localPosition = 
        new Vector3(mouseX+dX,mouseY+dY);

        Camera camera = Camera.main;

        setOpacity(origin,0.5f);
    }

    void setOpacity(GameObject obj,float opacity){
        CanvasGroup elem = obj.GetComponent<CanvasGroup>();
        if (elem==null) return;

        elem.alpha = opacity;
    }

}

[System.Serializable]
public delegate void onDragEnd(float dropX, float dropY);
