using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    public GameObject GameMap;
    public float speed=5;

    string movingMode = "mouse";

    static public float cosX=0;
    static public float sinY=0;

    private void Start() {
        findMoveMode();
    }
    void Update()
    {
        if (movingMode=="mouse"){
            findDirectionMouse();
        }else{
            findDirectionKeyboard();
        }

        if (!(MoveController.cosX==0 && MoveController.sinY==0)){
            moveUser();
        }
    }

    void findDirectionKeyboard(){
        float x = 
        MoveController.cosX>0 ? 1:
        MoveController.cosX<0 ?-1: 0;

        float y = 
        MoveController.sinY>0 ? 1:
        MoveController.sinY<0 ?-1: 0;


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
            MoveController.cosX=0;
            MoveController.sinY=0;

            return;
        }


        float Radius = Mathf.Sqrt(Mathf.Pow(x,2)+Mathf.Pow(y,2));

        MoveController.cosX=x/Radius;
        MoveController.sinY=y/Radius;
    }

    void findDirectionMouse(){
        if (!Input.GetMouseButton(0)){
            MoveController.cosX=0;
            MoveController.sinY=0;
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
        
        MoveController.cosX=-deltaDX/Radius;
        MoveController.sinY=-deltaDY/Radius;
    }

    void moveUser(){
        float cosX = MoveController.cosX;
        float sinY = MoveController.sinY;

        float deltaX = cosX*this.speed*Time.deltaTime;
        float deltaY = sinY*this.speed*Time.deltaTime;

        transform.Translate(new Vector2(deltaX,deltaY));
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
