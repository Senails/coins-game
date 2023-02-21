using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // find size component
    public static float getWidth(Transform transform){
        return transform.GetComponent<RectTransform>().rect.width;
    }
    public static float getHeight(Transform transform){
        return transform.GetComponent<RectTransform>().rect.height;
    }

    
    public static float getScaleX(Transform transform){
        return transform.localScale.x;
    }
    public static float getScaleY(Transform transform){
        return transform.localScale.y;
    }


    // need for setPosition functions
    static float getNeedY(Transform transform, float num){
        float height = Utils.getHeight(transform);
        float parentHeight = Utils.getHeight(transform.parent);

        return parentHeight/2-num-height/2;
    }
    static float getNeedX(Transform transform, float num){
        float width = Utils.getWidth(transform);
        float parentWidth = Utils.getWidth(transform.parent);

        return parentWidth/2-num-width/2;
    }

    //move object
    public static void setPosition(Transform transform, float x , float y){
        float ParentScaleX = Utils.getScaleX(transform.parent);
        float ParentScaleY = Utils.getScaleY(transform.parent);

        transform.position = new Vector2(x*ParentScaleX,y*ParentScaleY);
    }

    // work like as top,left,... in CSS
    public static void setPositionTop(Transform transform, float num){
        Utils.setPosition(transform,transform.position.x/Utils.getScaleX(transform.parent) ,Utils.getNeedY(transform,num));
    }
    public static void setPositionBottom(Transform transform, float num){
        Utils.setPosition(transform,transform.position.x/Utils.getScaleX(transform.parent) ,-Utils.getNeedY(transform,num));
    }
    public static void setPositionLeft(Transform transform, float num){
        Utils.setPosition(transform,-Utils.getNeedX(transform,num) , transform.position.y/Utils.getScaleY(transform.parent));
    }
    public static void setPositionRight(Transform transform, float num){
        Utils.setPosition(transform,Utils.getNeedX(transform,num), transform.position.y/Utils.getScaleY(transform.parent));
    }

    //show/hide components
    public static void showComponent(Transform transform){
        transform.gameObject.SetActive(true);
    }
    public static void hideComponent(Transform transform){
        transform.gameObject.SetActive(false);
    }
}
