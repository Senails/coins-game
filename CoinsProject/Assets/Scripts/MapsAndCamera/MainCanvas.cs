using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    public static Canvas Self;
    private void Awake() {
        Self = gameObject.GetComponent<Canvas>();
    }
}
