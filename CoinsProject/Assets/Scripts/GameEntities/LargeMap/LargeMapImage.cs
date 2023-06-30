using UnityEngine;
using UnityEngine.EventSystems;


using static UiCordsLib;
using static GameCordsLib;


public class LargeMapImage : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public LargeMapWindow MapWindow;
    public RectTransform Rect;


    private bool _hover=false;
    private bool _dragable=false;


    private Vector2 _mouseStartPosition;
    private Vector2 _cameraStartPosition;
    

    public void Start(){   
        Rect = GetComponent<RectTransform>();
        Vector2 sizes = GetUiSizes(transform.parent.gameObject);
        float size = Mathf.Max(sizes.x,sizes.y);
        Rect.sizeDelta = new Vector2(size,size);
    }
    public void Update() {
        if (!_hover) return;
        ResizeCamera();

        if (Input.GetMouseButtonDown(0)) DragStart();
        if (Input.GetMouseButtonUp(0)) DragEnd();
        if (_dragable) DragMove();
    }


    private void DragStart(){
        _dragable = true;
        _mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _cameraStartPosition = MapWindow.MapCamera.transform.position;
    }
    private void DragEnd(){
        _dragable = false;
    }
    private void DragMove(){
        Vector2 mouseNowPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDeltaPosition = mouseNowPosition - _mouseStartPosition;

        Vector2 MapImageSizes = GetSizesGameObject(gameObject);
        Vector2 MapCameraSizes = GetSizesGameObject(MapWindow.MapCamera.GetComponent<Camera>());


        Debug.Log(MapImageSizes);

        Vector2 newCameraCords = _cameraStartPosition - mouseDeltaPosition;
        MapWindow.MapCamera.SetPosition(newCameraCords);
    }


    private void ResizeCamera(){
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(mw)>0){
            if (mw<0){
                MapWindow.MapCamera.ChangeCameraSize(true);
            }else{
                MapWindow.MapCamera.ChangeCameraSize(false);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData){ 
        _hover = true;
    }
    public void OnPointerExit(PointerEventData eventData){ 
        _hover = false;
        DragEnd();
    }
}
