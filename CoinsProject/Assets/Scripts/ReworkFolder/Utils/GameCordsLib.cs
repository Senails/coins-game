using System;
using UnityEngine;

public static class GameCordsLib{
    public static Vector2 RandomCordsInGameRect(GameObject rect){
        BorderCords borderCords = GetBorderCordsGameRect(rect);
        float randX = UnityEngine.Random.Range(borderCords.MinX,borderCords.MaxX);
        float randY = UnityEngine.Random.Range(borderCords.MinY,borderCords.MaxY);
        return new Vector2(randX,randY);
    }
    public static bool CheckCordsInGameRect(GameObject rect,float x , float y){
        BorderCords borderCords = GetBorderCordsGameRect(rect);
        if (x>borderCords.MaxX || x<borderCords.MinX) return false;
        if (y>borderCords.MaxY || y<borderCords.MinY) return false;
        return true;
    }


    public static Vector2 GetSizesGameObject(GameObject obj){
        return new Vector2{
            x = obj.transform.localScale.x,
            y = obj.transform.localScale.y
        };
    }
    public static Vector2 GetSizesGameObject(Camera camera){
        float heightCamera = camera.orthographicSize*2;

        return new Vector2{
            x = camera.aspect*heightCamera,
            y = heightCamera
        };
    }


    public static BorderCords GetBorderCordsGameRect(GameObject rect){
        float rectWidthPart = rect.transform.localScale.x/2;
        float rectHeightPart = rect.transform.localScale.y/2;

        float rectX = rect.transform.position.x;
        float rectY = rect.transform.position.y;

        return new BorderCords{
            MinX = rectX-rectWidthPart,
            MaxX = rectX+rectWidthPart,
            MinY = rectY-rectHeightPart,
            MaxY = rectY+rectHeightPart
        };
    }
    public static BorderCords GetBorderCordsForCamera(GameObject rect){
        if (rect==null){
            return new BorderCords{
                MaxX=9999999,
                MinX=-9999999,
                MaxY=9999999,
                MinY=-9999999,
            };
        }

        BorderCords cords = GetBorderCordsGameRect(rect);

        Camera camera = Camera.main;
        float heightCamera = camera.orthographicSize*2;
        float widthCamera = camera.aspect*heightCamera;

        cords.MaxX -= widthCamera/2;
        cords.MinX += widthCamera/2;
        cords.MaxY -= heightCamera/2;
        cords.MinY += heightCamera/2;

        return cords;
    }


    public static bool CheckFreePosition(Vector2 cords, float radius){
        Collider2D check = Physics2D.OverlapCircle(cords,1);
        if (check==null) return true;
        return false;
    }
    public static Vector2 FindFreeCordsInGameRect(GameObject rect,float freeRadius){
        int counter = 0;

        while (true){
            Vector2 cords = RandomCordsInGameRect(rect);
            bool res = CheckFreePosition(cords,freeRadius);
            if (res) return cords;
            counter++;
            if (counter>200) break;
        }

        throw new Exception("Ошибка при поиске места для спавна");
    }






    public record BorderCords{
        public float MinX = 0;
        public float MaxX = 0;
        public float MinY = 0;
        public float MaxY = 0;
    };
    public record Sizes{
        public float Width = 0;
        public float Height = 0;
    };
}

namespace System.Runtime.CompilerServices
{
        internal static class IsExternalInit {}
}