using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject GameMap;
    public GameObject UserObject;

    public float rigidityPosition = 20;
    public float rigiditySpeed = 10;

    float minCameraX;
    float maxCameraX;
    float minCameraY;
    float maxCameraY; 
    
    float StartCameraSize;

    static CameraScript Self;

    bool CameraMoved=false;

    void Start()
    {   
        findMaxMinCameraPosition();
        getStartCameraSize();

        Self = this;
    }

    void Update()
    {
        CameraMoved=false;

        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(mw)>0){
            if (mw<0){
                changeCameraSize("more");
            }else{
                changeCameraSize("less");
            }

            findMaxMinCameraPosition();
            moveCamera();
        }

        moveCamera();
    }

    void findMaxMinCameraPosition(){
        Camera camera = Camera.main;

        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        float mapWidth = GameMap.transform.localScale.x;
        float mapHeight = GameMap.transform.localScale.y;

        this.minCameraX = (widthCamera-mapWidth)/2+GameMap.transform.position.x;
        this.maxCameraX = -((widthCamera-mapWidth)/2-GameMap.transform.position.x);

        this.minCameraY = (heightCamera-mapHeight)/2+GameMap.transform.position.y;
        this.maxCameraY = -((heightCamera-mapHeight)/2-GameMap.transform.position.y);
    }

    void moveCamera(){
        if (CameraMoved) return;
        CameraMoved=true;

        Camera camera = Camera.main;

        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        float xCamera = camera.transform.position.x;
        float yCamera = camera.transform.position.y;

        float xUser = UserObject.transform.position.x;
        float yUser = UserObject.transform.position.y;

        float deltaX = xUser-xCamera;
        float deltaY = yUser-yCamera;

        float needCameraX = xCamera;
        float needCameraY = yCamera;

        if (Mathf.Abs(deltaX) > widthCamera/rigidityPosition){
            needCameraX = (deltaX>0)?
            (xUser-widthCamera/(rigidityPosition)):
            (xUser+widthCamera/(rigidityPosition));
        }

        if (Mathf.Abs(deltaY) > heightCamera/rigidityPosition){
            needCameraY = (deltaY>0)?
            (yUser-heightCamera/rigidityPosition):
            (yUser+heightCamera/rigidityPosition);
        }

        needCameraX = 
        (needCameraX>=maxCameraX)?maxCameraX:
        (needCameraX<=minCameraX)?minCameraX:needCameraX;

        needCameraY = 
        (needCameraY>=maxCameraY)?maxCameraY:
        (needCameraY<=minCameraY)?minCameraY:needCameraY;

        float calculateX = xCamera + (needCameraX-xCamera)/(1000/rigiditySpeed);
        float calculateY = yCamera + (needCameraY-yCamera)/(1000/rigiditySpeed);

        Camera.main.transform.position= new Vector3(calculateX,calculateY,-10);
    }

    void getStartCameraSize(){
        var camera = transform.GetComponent<Camera>();
        StartCameraSize = camera.orthographicSize;
    }

    void changeCameraSize(string MoreLess){
        var camera = transform.GetComponent<Camera>();
        float size = camera.orthographicSize;;

        if (MoreLess == "less"){
            size= size*.95f;
        }else{
            size= size*1.05f;
        }

        if (size>this.StartCameraSize*2){
            size=this.StartCameraSize*2;
        }
        if (size<this.StartCameraSize/2){
            size=this.StartCameraSize/2;
        }

        camera.orthographicSize=size;
    }

    static public void MoveCamera(){
        CameraScript.Self.moveCamera();
    }
}
