using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinieButton : MonoBehaviour
{
    void Start()
    {
        if (GlobalStateSaveMeneger.SaveConfig.ActiveLocationID == 0) return;
        Button but = gameObject.GetComponent<Button>();
        but.interactable = true;
    }
}
