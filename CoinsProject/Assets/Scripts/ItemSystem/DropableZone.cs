using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropableZone : MonoBehaviour
{
    static DropableZone Self;
    GameObject clone; 
    private void Start() {
        DropableZone.Self = this;
        this.gameObject.SetActive(false);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)){
            pointerUp();
            return;
        }

        pointerMove();
    }

    static public void dragStart(GameObject obj){
        Self.gameObject.SetActive(true);
        Self.clone = Object.Instantiate(obj,Self.transform);
    }


    public void pointerUp()
    {   
        Debug.Log($"{clone}");

        // Destroy(clone);
        Self.gameObject.SetActive(false);
    }

    public void pointerMove(){ 
        float mouseDX = Input.mousePosition.x/Screen.width;
        float mouseDY = Input.mousePosition.y/Screen.height;

        RectTransform rectTr = transform.GetComponent<RectTransform>();
        Rect rect = rectTr.rect;

        float width = rect.width;
        float height = rect.height;

        float needX = width*mouseDX;
        float needY = height*mouseDY;


        clone.transform.localPosition = new Vector2(100,100);
    }
}
