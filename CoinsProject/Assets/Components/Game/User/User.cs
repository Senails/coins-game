using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public float speed;

    void Start()
    {

    }

    void Update()
    {   
        if (!(MoveController.cosX==0 && MoveController.sinY==0)){
            moveUser();
        }
    }

    void moveUser(){
        float cosX = MoveController.cosX;
        float sinY = MoveController.sinY;

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
        (needCameraX>=MoveController.maxCameraX)?MoveController.maxCameraX:
        (needCameraX<=MoveController.minCameraX)?MoveController.minCameraX:needCameraX;

        needCameraY = 
        (needCameraY>=MoveController.maxCameraY)?MoveController.maxCameraY:
        (needCameraY<=MoveController.minCameraY)?MoveController.minCameraY:needCameraY;

        Camera.main.transform.position= new Vector3(needCameraX,needCameraY,-10);
    }

}
