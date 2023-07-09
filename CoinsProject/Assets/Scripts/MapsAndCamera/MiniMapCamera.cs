using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(Player.Self.transform.position.x,Player.Self.transform.position.y,-10);
    }
}
