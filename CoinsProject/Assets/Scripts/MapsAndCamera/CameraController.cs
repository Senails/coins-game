using UnityEngine;
using UnityEngine.EventSystems;


using static GameCordsLib;
using static AsyncLib;


public class CameraController : MonoBehaviour
{
    public static CameraController Self;
    public GameObject BorderRectForCamera;
    public float NormalCameraSize;
    

    private Player _playerObject;
    private Rigidbody2D _rb;
    private Vector2 _virtualCameraPosition;
    
    
    private void Start(){
        _virtualCameraPosition = Camera.main.transform.position;
        NormalCameraSize = Camera.main.orthographicSize;
        _rb = this.GetComponent<Rigidbody2D>();

        _playerObject = Player.Self;
        Self = this;
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

        // Vector2 needCords = (userCords - _virtualCameraPosition)*2 + _virtualCameraPosition;
        Vector2 needCords = _virtualCameraPosition;

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
                needSize = Mathf.Clamp(needSize,NormalCameraSize/2,NormalCameraSize*2);

                Camera.main.orthographicSize = needSize;
                MoveCamera();
            },i*30);
        }
    }
    private void CalculateNewCameraPosition(){
        int constNum = 16;

        Vector2 cameraSizes = GetSizesGameObject(Camera.main);

        Vector2 user = _playerObject.transform.position; 
        Vector2 camera = _virtualCameraPosition; 


        camera.x = Mathf.Clamp(camera.x,user.x-cameraSizes.x/constNum,user.x+cameraSizes.x/constNum);
        camera.y = Mathf.Clamp(camera.y,user.y-cameraSizes.y/constNum,user.y+cameraSizes.y/constNum);


        if ((user-camera).magnitude > (cameraSizes/(constNum*5)).magnitude){
            camera = camera + (user-camera)*Time.deltaTime;
        }
        _virtualCameraPosition = camera;
    }
}
