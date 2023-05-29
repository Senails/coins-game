using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


using static GameCordsLib;

public static class UiCordsLib{
    private static GameObject _canvas = null;


    public static Vector2 findMousePositionAboutCanvas(){
        Vector2 mouseRelativeCords = GetMouseCordsAboutScreen();
        Vector2 canvasSizes = GetUiSizes(getCanvas());    

        return new Vector2{
            x = mouseRelativeCords.x*canvasSizes.x,
            y = mouseRelativeCords.y*canvasSizes.y,
        };
    }
    public static Vector2 findUIPositionAboutCanvas(GameObject obj){
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
    public static bool CheckCordsInRect(float x, float y,GameObject obj){
        Vector2 objUiCords = findUIPositionAboutCanvas(obj);
        Vector2 objUiSizes = GetUiSizes(obj);

        if (Mathf.Abs(objUiCords.x-x)>objUiSizes.x/2) return false;
        if (Mathf.Abs(objUiCords.y-y)>objUiSizes.y/2) return false;
        return true;
    }
    



    private static Vector2 GetMouseCordsAboutScreen(){
        return new Vector2{
            x = Input.mousePosition.x/Screen.width-0.5f,
            y = Input.mousePosition.y/Screen.height-0.5f,
        };
    }
    private static GameObject getCanvas(){
        if (_canvas!=null) return _canvas;

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        _canvas = Array.Find<GameObject>(rootObjects,(elem)=>{
            Canvas can = elem.GetComponent<Canvas>();
            return can != null;
        });

        return _canvas;
    }
}