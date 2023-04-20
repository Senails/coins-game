using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PositionText : MonoBehaviour
{
    public GameObject User;

    static public PositionText Self;

    float startX = 0;
    float startY = 0;

    private void Start() {
        startX = User.transform.position.x;
        startY = User.transform.position.y;

        Self = this;

        PositionText.updatePosition();
    }

    static public void updatePosition()
    {
        TMP_Text text = Self.GetComponent<TMPro.TMP_Text>();

        float userX = Self.User.transform.position.x;
        float userY = Self.User.transform.position.y;

        float showX = userX - Self.startX;
        float showY = userY - Self.startY;

        text.text=$"x = {showX} y = {showY}";
    }
}
