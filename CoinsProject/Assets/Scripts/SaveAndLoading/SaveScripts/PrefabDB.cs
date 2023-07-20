using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDB : MonoBehaviour
{
    public static PrefabDB Self;
    public List<GameObject> PrefabListDB;

    private void Awake() {
        Self = this;
    }
}
