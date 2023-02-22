using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static float cosX=0;
    public static float sinY=0;

    void Update()
    {
        findDirectionKeyboard();
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
}
