using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOptionsUI : MonoBehaviour
{
    public GameObject LineObjectPrefab;

    private void Start() {
        Render();
    }
    private void OnEnable() {
        Render();
    }

    public void Render(){
        RemoveChildrens();
        Dictionary<string,KeyCode> KyeDictionary = OptionsManager.Config.KyeDictionary;

        foreach(var elem in KyeDictionary){
            GameObject obj = UnityEngine.Object.Instantiate(LineObjectPrefab,transform);
            KeyOptionLine line = obj.GetComponent<KeyOptionLine>();

            line.Init(elem.Key,elem.Value);
        }
    }

    public void RemoveChildrens(){
        for(int i = transform.childCount-1;i>-1;i--){
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
