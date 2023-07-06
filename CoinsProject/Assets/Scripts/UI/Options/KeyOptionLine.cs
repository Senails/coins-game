using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static KeyUtils;

public class KeyOptionLine : MonoBehaviour
{   
    public TMP_Text ActionNameText;
    public TMP_Text KeyNameText;
    public CanvasGroup image;
    public GameObject CancelButton;


    private static bool SomethingWantChageKey = false;
    private bool IWantChageKey = false;


    private string ActionName;


    public void Init(string name , KeyCode code){
        ActionNameText.text = name;
        KeyNameText.text = GetNameKey(code);
        SomethingWantChageKey = false;

        ActionName = name;
    }
    public void Update() {
        if (IWantChageKey && Input.anyKeyDown){
            TryChangeKey();
        }
    }
    public void OnDisable() {
        EndChangeKey();
    }


    public void StartChangeKey(){
        if (SomethingWantChageKey) return;
        SomethingWantChageKey = true;
        IWantChageKey = true;

        KeyNameText.text = "нажмите клавишу";
        image.alpha = 1;
        CancelButton.SetActive(true);
    }
    public void EndChangeKey(){
        if (!IWantChageKey) return;
        SomethingWantChageKey = false;
        IWantChageKey = false;

        KeyNameText.text = GetNameKey(OptionsManager.Config.KyeDictionary[ActionName]);
        image.alpha = 0.8f;
        CancelButton.SetActive(false);
    }


    public void TryChangeKey(){
        List<KeyCode> list = GetPressedKeys();
        if (list.Count==0){
            KeyNameText.text = "клавиша не доступна";
            return;
        }

        if (OptionsManager.Config.KyeDictionary.ContainsValue(list[0])){
            KeyNameText.text = "клавиша занята";
            return;
        }

        OptionsManager.Self.SetActionKey(ActionName,list[0]);
        EndChangeKey();
    }
}
