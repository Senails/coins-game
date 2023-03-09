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
    float activeCameraSize;

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

        resizeCameraHandler();
        moveCamera();
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
            (xUser-widthCamera/rigidityPosition):
            (xUser+widthCamera/rigidityPosition);
        }

        if (Mathf.Abs(deltaY) > heightCamera/rigidityPosition){
            needCameraY = (deltaY>0)?
            (yUser-heightCamera/rigidityPosition):
            (yUser+heightCamera/rigidityPosition);
        }


        needCameraX = xCamera + (needCameraX-xCamera)/(1000/rigiditySpeed);
        needCameraY = yCamera + (needCameraY-yCamera)/(1000/rigiditySpeed);


        needCameraX = 
        (needCameraX>maxCameraX)?maxCameraX:
        (needCameraX<minCameraX)?minCameraX:needCameraX;


        needCameraY = 
        (needCameraY>maxCameraY)?maxCameraY:
        (needCameraY<minCameraY)?minCameraY:needCameraY;


        Camera.main.transform.position= new Vector3(needCameraX,needCameraY,-10);
    }
    void resizeCameraHandler(){
        float mw = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(mw)>0){
            if (mw<0){
                changeActiveCameraSize("more");
            }else{
                changeActiveCameraSize("less");
            }
        }
        changeRealCameraSize();
    }


    void getStartCameraSize(){
        var camera = transform.GetComponent<Camera>();

        StartCameraSize = camera.orthographicSize;
        activeCameraSize = camera.orthographicSize;
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
    void changeActiveCameraSize(string MoreLess){
        float size = this.activeCameraSize;

        if (MoreLess == "less"){
            size= size*0.90f;
        }else{
            size= size*1.10f;
        }

        if (size>this.StartCameraSize*1.5f){
            size=this.StartCameraSize*1.5f;
        }
        if (size<this.StartCameraSize/2){
            size=this.StartCameraSize/2;
        }

        this.activeCameraSize=size;
    }
    void changeRealCameraSize(){
        var camera = transform.GetComponent<Camera>();
        float size = camera.orthographicSize;

        if (Mathf.Abs(this.activeCameraSize-size)>0.001){
            size=size+(this.activeCameraSize-size)/((1/Time.deltaTime)/10);


            camera.orthographicSize=size;
            findMaxMinCameraPosition();
        }

    }

}
