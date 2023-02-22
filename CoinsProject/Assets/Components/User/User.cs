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
        moveUser();
    }

    void moveUser(){
        float cosX = MoveController.cosX;
        float sinY = MoveController.sinY;

        float deltaX = cosX*this.speed*Time.deltaTime;
        float deltaY = sinY*this.speed*Time.deltaTime;

        transform.Translate(new Vector2(deltaX,deltaY));
    }
}
