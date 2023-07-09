using UnityEngine;
using UnityEngine.EventSystems;


using static GameCordsLib;
using static AsyncLib;


public class CameraController : MonoBehaviour
{
    public GameObject BorderRectForCamera;
    
    private Player _playerObject;
    private Rigidbody2D _rb;
    private Vector2 _virtualCameraPosition;
    private float _normalCameraSize;
    

    private void Start(){
        _virtualCameraPosition = Camera.main.transform.position;
        _normalCameraSize = Camera.main.orthographicSize;
        _rb = this.GetComponent<Rigidbody2D>();

        _playerObject = Player.Self;
    }
    private void LateUpdate () {
        if (GameMeneger.Status == GameMeneger.GameStatus.pause) return;

        ResizeCamera();
        CalculateNewCameraPosition();
        MoveCamera();
    }
    

    private void MoveCamera(){
        Vector2 userCords = _playerObject.transform.position; 
        BorderCords borderCords = GetBorderCordsForCamera(this.BorderRectForCamera);

        Vector2 needCords = (userCords - _virtualCameraPosition)*2 + _virtualCameraPosition;

        needCords.x = Mathf.Clamp(needCords.x,borderCords.MinX,borderCords.MaxX);
        needCords.y = Mathf.Clamp(needCords.y,borderCords.MinY,borderCords.MaxY);

        _rb.position = needCords;
    }
    private void ResizeCamera(){
        if (EventSystem.current.IsPointerOverGameObject()) return;
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(mw)>0){
            if (mw<0){
                ChangeCameraSize(true);
            }else{
                ChangeCameraSize(false);
            }
        }
    }


    private void ChangeCameraSize(bool more){
        for(int i=0; i<10; i++){
            setTimeout(()=>{
                float needSize = Camera.main.orthographicSize;

                needSize = more?needSize*(1 + 0.01f):needSize*(1 - 0.01f);
                needSize = Mathf.Clamp(needSize,_normalCameraSize/2,_normalCameraSize*2);

                Camera.main.orthographicSize = needSize;
                MoveCamera();
            },i*30);
        }
    }
    private void CalculateNewCameraPosition(){
        Vector2 cameraSizes = GetSizesGameObject(Camera.main);

        Vector2 user = _playerObject.transform.position; 
        Vector2 camera = _virtualCameraPosition; 


        camera.x = Mathf.Clamp(camera.x,user.x-cameraSizes.x/8,user.x+cameraSizes.x/8);
        camera.y = Mathf.Clamp(camera.y,user.y-cameraSizes.y/8,user.y+cameraSizes.y/8);


        if ((user-camera).magnitude > (cameraSizes/40).magnitude){
            camera = camera + (user-camera)*Time.deltaTime;
        }
        _virtualCameraPosition = camera;
    }
}
