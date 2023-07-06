using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursivDisable : MonoBehaviour
{
    private void OnDisable() {
        gameObject.SetActive(false);
    }
}
