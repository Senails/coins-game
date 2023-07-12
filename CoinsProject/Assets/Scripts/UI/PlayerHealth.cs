using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public RectTransform HealthIndicatorLine;
    
    void Start()
    {
        Player.Self.OnChengeHealth+=Render;
        Render(Player.Self.Health,Player.Self.MaxHealth);
    }

    void Render(int Health , int MaxHealth){
        float parentWidth = HealthIndicatorLine.parent.GetComponent<RectTransform>().rect.size.x;
        float needWidthLine = ((float)Health/(float)MaxHealth)*parentWidth;
        HealthIndicatorLine.sizeDelta = new Vector2(needWidthLine,HealthIndicatorLine.rect.size.y);
    }
}
