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
        findDirection();

        if (!(MoveController.cosX==0 && MoveController.sinY==0)){
            moveUser();
        }
    }


    void findDirection(){
        if (movingMode=="hybrid"){
            findDirectionKeyboard();

            if (MoveController.cosX==0 
            && MoveController.sinY==0){
                findDirectionMouse();
            }
        }else if (movingMode=="keyboard"){
            findDirectionKeyboard();
        }else{
            findDirectionMouse();
        }
    }
    
    void moveUser(){
        float cosX = MoveController.cosX;
        float sinY = MoveController.sinY;

        float deltaX = cosX*this.speed*Time.deltaTime;
        float deltaY = sinY*this.speed*Time.deltaTime;

        transform.Translate(new Vector2(deltaX,deltaY));
        CameraScript.MoveCamera();
        MiniMap.changeMiniMap();
    }



    void findDirectionKeyboard(){
        float x = findDirectionKeyboardX();

        float y = findDirectionKeyboardY();

        if (x==0 && y ==0 ){
            changeDirection(0,0);
            return;
        }

        float Radius = Mathf.Sqrt(Mathf.Pow(x,2)+Mathf.Pow(y,2));

        changeDirection(x/Radius,y/Radius);
    }
    
    int findDirectionKeyboardY(){
        if (!Input.GetKey(KeyCode.W) 
        && !Input.GetKey(KeyCode.S)) return 0;

        if (Input.GetKey(KeyCode.W) 
        && Input.GetKey(KeyCode.S)) return 0;

        if (Input.GetKey(KeyCode.S)){
            return -1;
        }

        return 1;
    }
   
    int findDirectionKeyboardX(){
        if (!Input.GetKey(KeyCode.A) 
        && !Input.GetKey(KeyCode.D)) return 0;

        if (Input.GetKey(KeyCode.A) 
        && Input.GetKey(KeyCode.D)) return 0;

        if (Input.GetKey(KeyCode.A)){
            return -1;
        }

        return 1;
    }



    void findDirectionMouse(){
        if (!Input.GetMouseButton(0)){
            changeDirection(0,0);
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
        
        if (Radius<0.01){
            changeDirection(0,0);
            return;
        }

        changeDirection(-deltaDX/Radius,-deltaDY/Radius);
    }

    void findMoveMode(){
        int index = PlayerPrefs.GetInt("moveMode");

        if (index==0){
            movingMode="keyboard";  
        }else if (index==1){
            movingMode="mouse"; 
        }else{
            movingMode="hybrid";
        }

        Debug.Log(movingMode);
    }

    void changeDirection(float cos, float sin){
        if (cos!=cosX || sin!=sinY){
            MoveController.cosX=cos;
            MoveController.sinY=sin;

            UserImageController.recalculateImage();
        }
    }

}
