using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using static AsyncLib;

public class LargeMapCamera : MonoBehaviour
{
    private Camera _selfCamera;
    private float _normalCameraSize;


    private void Start(){
        _selfCamera = GetComponent<Camera>();
        _normalCameraSize = _selfCamera.orthographicSize;
    }


    public void ChangeCameraSize(bool more){
        for(int i=0; i<10; i++){
            setTimeout(()=>{
                float needSize = _selfCamera.orthographicSize;

                needSize = more?needSize*(1 + 0.01f):needSize*(1 - 0.01f);
                needSize = Mathf.Clamp(needSize,_normalCameraSize/2,_normalCameraSize*2);

                _selfCamera.orthographicSize = needSize;
            },i*30);
        }
    }
    public void SetPosition(Vector2 position){
        transform.position = new Vector3(position.x,position.y,-10);
    }
}
