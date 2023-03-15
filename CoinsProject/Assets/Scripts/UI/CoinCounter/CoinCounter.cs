using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    int countCoins;
    static CoinCounter Self;
    public TMP_Text textBlock;
    bool visible = false;

    double lastChanges;

    private void Start() {
        Self = this;
        countCoins=0;
        textBlock.text = countCoins.ToString();

        lastChanges = Time.timeAsDouble;
    }

    private void Update() {
        if (!visible) return;
        if ((Time.timeAsDouble-lastChanges)>3){
            hideObject();
        }
    }

    static public void addCoins(int count){
        Self.showObject();
        Self.lastChanges = Time.timeAsDouble;
        Self.countCoins+=count;
        Self.textBlock.text = Self.countCoins.ToString();
    }
    static public void removeCoins(int count){
        Debug.Log($"Remove coins {count}");

        Self.showObject();
        Self.lastChanges = Time.timeAsDouble;
        Self.countCoins-=count;
        Self.textBlock.text = Self.countCoins.ToString();
    }

    void hideObject(){
        if (this.visible){
            Image image = transform.GetComponent<Image>();
            image.color = new Color(1,1,1,0);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);

            this.visible=false;
        }
    }

    void showObject(){
        if (!this.visible){
            Image image = transform.GetComponent<Image>();
            image.color = new Color(1,1,1,1);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);

            this.visible=true;
        }
    }


}
