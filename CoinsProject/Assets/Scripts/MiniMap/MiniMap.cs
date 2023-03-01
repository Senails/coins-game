using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject ImageMap;
    public GameObject GameMap;
    public GameObject User;

    static MiniMap Self;


    void Start()
    {
        MiniMap.Self = this;
        moveMiniMap();
    }

    void findPosition(ref float x , ref float y){
        float MapX = GameMap.transform.position.x;
        float MapY = GameMap.transform.position.y;

        float MapWidth = GameMap.transform.localScale.x;
        float MapHeight = GameMap.transform.localScale.y;

        float UserX = User.transform.position.x;
        float UserY = User.transform.position.y;

        float MapDX = (UserX - MapX)/MapWidth;
        float MapDY = (UserY - MapY)/MapHeight;

        x = MapDX;
        y = MapDY;
    }

    void moveMiniMap(){
        float x = 0;
        float y = 0;

        findPosition(ref x,ref y);
        var rectTransform = ImageMap.GetComponent<RectTransform>();

        float MiniMapWidth = rectTransform.rect.width;
        float MiniMapHeight = rectTransform.rect.height;

        float needX = -x*MiniMapWidth;
        float needY = -y*MiniMapHeight;

        ImageMap.transform.localPosition= new Vector2(needX,needY);
    }

    static public void changeMiniMap(){
        MiniMap.Self.moveMiniMap();
    }
}
