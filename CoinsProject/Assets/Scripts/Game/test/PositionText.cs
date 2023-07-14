using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PositionText : MonoBehaviour
{
    private GameObject _player;


    float startX = 0;
    float startY = 0;

    private void Start() {
        if (Player.Self==null) return;
        startX = Player.Self.transform.position.x;
        startY = Player.Self.transform.position.y;

        UpdatePosition();
    }
    private void Update(){
        if (Player.Self==null) return;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        TMP_Text text = this.GetComponent<TMPro.TMP_Text>();

        float userX = Player.Self.transform.position.x;
        float userY = Player.Self.transform.position.y;

        float showX = userX - this.startX;
        float showY = userY - this.startY;

        text.text=$"x = {showX} y = {showY}";
    }
}
