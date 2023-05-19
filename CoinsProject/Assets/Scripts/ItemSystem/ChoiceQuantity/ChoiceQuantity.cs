using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceQuantity : MonoBehaviour
{
    static public ChoiceQuantity Self;
    public InventoryStatus status = InventoryStatus.hide;

    public GameObject inputRange;
    public GameObject textBlock;

    int maxItemCount;
    int activeCount;

    onChoiceCount callback;

    private void Update() {
        if (status == InventoryStatus.hide) return;
        Slider slider = inputRange.GetComponent<Slider>();

        maxItemCount = (int)Mathf.Floor(slider.maxValue);
        activeCount = (int)Mathf.Floor(slider.value);

        render();
    }

    static public void showPanel(){
        Self.gameObject.SetActive(true);
        Self.status = InventoryStatus.show;
        GameMeneger.PauseGame();
    }

    static public void hidePanel(){
        Self.gameObject.SetActive(false);
        Self.status = InventoryStatus.hide;
        GameMeneger.PlayGame();
    }

    static public void MoveItem(){
        hidePanel();
        Self.callback(Self.activeCount);
    }

    static public void initPanel(int count,onChoiceCount callback){
        showPanel();

        Self.maxItemCount=count;
        Self.activeCount=count;
        Self.callback=callback;

        initSlider(count);
        Self.render();
    }
    static public void initSlider(int count){
        Slider slider = Self.inputRange.GetComponent<Slider>();

        slider.maxValue = count;
        slider.value = count;
        Self.render();
    }

    void render(){
        TMP_Text TMP = textBlock.GetComponent<TMPro.TMP_Text>();
        TMP.text = $"{activeCount} / {maxItemCount}";
    }

}

[System.Serializable]
public delegate void onChoiceCount(int count);