using UnityEngine;
using UnityEngine.EventSystems;

using static UiCordsLib;

public class DragablePlace : MonoBehaviour, IPointerDownHandler
{
    private bool _active = false;
    private Vector2 _deltaCords;


    public GameObject UiWindow;


    public void OnPointerDown(PointerEventData eventData){ 
        if (Input.GetMouseButton(0)){
            _active = true;
            Vector2 windowCords = findUIPositionInCanvas(UiWindow);
            Vector2 mouseCords = findMousePositionInCanvas();
            _deltaCords = windowCords - mouseCords;
        }
    }


    public void Update(){
        if (!_active) return;
        if (Input.GetMouseButtonUp(0)){
            _active = false;
            return;
        }
        MovingWindow();
    }


    public void MovingWindow(){
        Vector2 mouseCords = findMousePositionInCanvas();
        Vector2 newPosition = mouseCords + _deltaCords;

        Vector2 canvasSizes = GetUiSizes(getCanvas());
        Vector2 windowSizes = GetUiSizes(UiWindow);

        float horizontalBorder = (canvasSizes.x - windowSizes.x)/2;
        float verticalBorder = (canvasSizes.y - windowSizes.y)/2;

        newPosition.x = Mathf.Clamp(newPosition.x,-horizontalBorder,horizontalBorder);
        newPosition.y = Mathf.Clamp(newPosition.y,-verticalBorder,verticalBorder);

        UiWindow.transform.localPosition = newPosition;
    }
}
