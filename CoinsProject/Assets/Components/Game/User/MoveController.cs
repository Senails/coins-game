using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    public GameObject GameMap;
    public float speed=5;

    string movingMode = "mouse";

    float cosX=0;
    float sinY=0;

    float minCameraX;
    float maxCameraX;
    float minCameraY;
    float maxCameraY;

    private void Start() {
        findMaxMinCameraPosition();
        findMoveMode();
    }
    void Update()
    {
        if (movingMode=="mouse"){
            findDirectionMouse();
        }else{
            findDirectionKeyboard();
        }

        if (!(this.cosX==0 && this.sinY==0)){
            moveUser();
        }
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


    void findDirectionKeyboard(){
        float x = 
        this.cosX>0 ? 1:
        this.cosX<0 ?-1: 0;

        float y = 
        this.sinY>0 ? 1:
        this.sinY<0 ?-1: 0;


        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)){
            y=0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
            x=0;
        }


        if (Input.GetKey(KeyCode.W) && sinY<=0){
            y=1;
        }
        if (Input.GetKey(KeyCode.A) && cosX>=0){
            x=-1;
        }
        if (Input.GetKey(KeyCode.S) && sinY>=0){
            y=-1;
        }
        if (Input.GetKey(KeyCode.D) && cosX<=0){
            x=1;
        }


        if (x==0 && y ==0 ){
            this.cosX=0;
            this.sinY=0;

            return;
        }


        float Radius = Mathf.Sqrt(Mathf.Pow(x,2)+Mathf.Pow(y,2));

        this.cosX=x/Radius;
        this.sinY=y/Radius;
    }

    void findDirectionMouse(){
        if (!Input.GetMouseButton(0)){
            this.cosX=0;
            this.sinY=0;
            return;
        }
       

        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float mouseDX = mouseX/Screen.width;
        float mouseDY = mouseY/Screen.height;


        Camera camera = Camera.main;

        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        float xCamera = camera.transform.position.x;
        float yCamera = camera.transform.position.y;

        float xUser = transform.position.x;
        float yUser = transform.position.y;

        float deltaX = xCamera-xUser;
        float deltaY = yCamera-yUser;

        float userDX = 1-(deltaX+widthCamera/2)/widthCamera;
        float userDY = 1-(deltaY+heightCamera/2)/heightCamera;


        float deltaDX = userDX-mouseDX;
        float deltaDY = userDY-mouseDY;

        float Radius = Mathf.Sqrt(Mathf.Pow(deltaDX,2)+Mathf.Pow(deltaDY,2));
        
        this.cosX=-deltaDX/Radius;
        this.sinY=-deltaDY/Radius;
    }


    void moveUser(){
        float cosX = this.cosX;
        float sinY = this.sinY;

        float deltaX = cosX*this.speed*Time.deltaTime;
        float deltaY = sinY*this.speed*Time.deltaTime;

        transform.Translate(new Vector2(deltaX,deltaY));
        moveCamera();
    }

    void moveCamera(){
        Camera camera = Camera.main;
        const float rigidity = 10;

        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        float xCamera = camera.transform.position.x;
        float yCamera = camera.transform.position.y;

        float xUser = transform.position.x;
        float yUser = transform.position.y;

        float deltaX = xUser-xCamera;
        float deltaY = yUser-yCamera;

        float needCameraX = xCamera;
        float needCameraY = yCamera;

        if (Mathf.Abs(deltaX) > widthCamera/rigidity){
            needCameraX = (deltaX>0)?
            (xUser-widthCamera/rigidity):
            (xUser+widthCamera/rigidity);
        }

        if (Mathf.Abs(deltaY) > heightCamera/rigidity){
            needCameraY = (deltaY>0)?
            (yUser-heightCamera/rigidity):
            (yUser+heightCamera/rigidity);
        }

        needCameraX = 
        (needCameraX>=maxCameraX)?maxCameraX:
        (needCameraX<=minCameraX)?minCameraX:needCameraX;

        needCameraY = 
        (needCameraY>=maxCameraY)?maxCameraY:
        (needCameraY<=minCameraY)?minCameraY:needCameraY;

        Camera.main.transform.position= new Vector3(needCameraX,needCameraY,-10);
    }


    void findMoveMode(){
        int index = PlayerPrefs.GetInt("moveMode");

        if (index==0){
            movingMode="keyboard";  
        }else{
            movingMode="mouse"; 
        }
    }
}
