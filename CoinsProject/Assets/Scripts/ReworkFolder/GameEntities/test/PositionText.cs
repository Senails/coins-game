using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PositionText : MonoBehaviour
{
    public GameObject Player;


    float startX = 0;
    float startY = 0;

    private void Start() {
        if (Player==null) return;
        startX = Player.transform.position.x;
        startY = Player.transform.position.y;

        updatePosition();
    }
    private void Update(){
        if (Player==null) return;
        updatePosition();
    }

    public void updatePosition()
    {
        TMP_Text text = this.GetComponent<TMPro.TMP_Text>();

        float userX = this.Player.transform.position.x;
        float userY = this.Player.transform.position.y;

        float showX = userX - this.startX;
        float showY = userY - this.startY;

        text.text=$"x = {showX} y = {showY}";
    }
}
