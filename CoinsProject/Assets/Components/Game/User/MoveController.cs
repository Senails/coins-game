using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    public GameObject GameMap;

    public static string movingMode = "mouse";

    public static float cosX=0;
    public static float sinY=0;

    public static float minCameraX;
    public static float maxCameraX;
    public static float minCameraY;
    public static float maxCameraY;

    private void Start() {
        findMaxMinCameraPosition();
    }

    void Update()
    {
        if (MoveController.movingMode=="mouse"){
            findDirectionMouse();
        }else{
            findDirectionKeyboard();
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

    void findMaxMinCameraPosition(){
        Camera camera = Camera.main;

        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        float mapWidth = GameMap.transform.localScale.x;
        float mapHeight = GameMap.transform.localScale.y;

        MoveController.minCameraX = (widthCamera-mapWidth)/2+GameMap.transform.position.x;
        MoveController.maxCameraX = -((widthCamera-mapWidth)/2-GameMap.transform.position.x);

        MoveController.minCameraY = (heightCamera-mapHeight)/2+GameMap.transform.position.y;
        MoveController.maxCameraY = -((heightCamera-mapHeight)/2-GameMap.transform.position.y);
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

}
