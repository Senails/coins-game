using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


using static GameCordsLib;

public static class UiCordsLib{
    public static Vector2 findMousePositionInCanvas(){
        Vector2 mouseRelativeCords = GetMouseCordsRelativeScreen();
        Vector2 canvasSizes = GetUiSizes(getCanvas());    

        return new Vector2{
            x = mouseRelativeCords.x*canvasSizes.x,
            y = mouseRelativeCords.y*canvasSizes.y,
        };
    }
    public static Vector2 findUIPositionInCanvas(GameObject obj){
        Vector2 cameraPosition = Camera.main.transform.position;
        Vector2 objPosition = obj.transform.position;

        Vector2 cameraSizes = GetSizesGameObject(Camera.main);

        Vector2 deltaPosition = objPosition-cameraPosition;
        deltaPosition.x /= cameraSizes.x;
        deltaPosition.y /= cameraSizes.y;

        Vector2 canvasUiSizes = GetUiSizes(getCanvas());
        return new Vector2{
            x = canvasUiSizes.x*deltaPosition.x,
            y = canvasUiSizes.y*deltaPosition.y,
        };
    }

    
    public static Vector2 GetUiSizes(GameObject uiObject){
        RectTransform rectTr = uiObject.GetComponent<RectTransform>();
        Rect rect = rectTr.rect;


        return new Vector2 {
            x = rect.width,
            y = rect.height
        };
    }
    public static Vector2 GetGlobalUiSizes(GameObject uiObject){
        RectTransform rectTransform = uiObject.GetComponent<RectTransform>();
        Vector2 localSize = rectTransform.rect.size;
        return rectTransform.TransformVector(localSize);
    }


    public static bool CheckCordsInUIRect(float x, float y,GameObject obj){
        Vector2 objUiCords = findUIPositionInCanvas(obj);
        Vector2 objUiSizes = GetUiSizes(obj);

        if (Mathf.Abs(objUiCords.x-x)>objUiSizes.x/2) return false;
        if (Mathf.Abs(objUiCords.y-y)>objUiSizes.y/2) return false;
        return true;
    }


    public static Vector2 GetMouseCordsRelativeScreen(){
        return new Vector2{
            x = Input.mousePosition.x/Screen.width-0.5f,
            y = Input.mousePosition.y/Screen.height-0.5f,
        };
    }
    public static GameObject getCanvas(){
        return MainCanvas.Self.gameObject;
    }
}