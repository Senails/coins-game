using UnityEngine;
using TMPro;

using static KeyUtils;

public class KeyOptionLine : MonoBehaviour
{   
    public TMP_Text ActionNameText;
    public TMP_Text KeyNameText;

    public void Init(string name , KeyCode code){
        ActionNameText.text = name;
        KeyNameText.text = GetNameKey(code);
    }
}
