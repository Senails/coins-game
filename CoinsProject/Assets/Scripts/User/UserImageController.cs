using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserImageController : MonoBehaviour
{
    int activeImage = 0;

    static UserImageController Self;

    void Start(){
        UserImageController.Self=this;
    }

    int findActiveImage(){
        float cosX = MoveController.cosX;
        float sinY = MoveController.sinY;

        if (cosX==0 && sinY==0){
            return 0;
        }

        if (Mathf.Abs(sinY) > Mathf.Abs(cosX)){
            return (sinY>0)?1:0;
        }else{
            return (cosX>0)?3:2;
        }
    }

    void activeNeedImage(int index){
        int childCount = transform.childCount;

        for(int i=0; i<childCount;i++){
            var child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }

        var needChild = transform.GetChild(index);
        needChild.gameObject.SetActive(true);

        activeImage=index;
    }

    static public void recalculateImage(){
        int imageIndex = Self.findActiveImage();
        if (imageIndex!=Self.activeImage){
            Self.activeNeedImage(imageIndex);
        }
    }
}
