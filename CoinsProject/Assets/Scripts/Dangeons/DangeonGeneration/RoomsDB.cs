using System.Collections.Generic;
using UnityEngine;

using DangeonsTypes;

public class RoomsDB : MonoBehaviour
{
    public List<DBRoom> RoomList;
    public static RoomsDB Self;

    private void Awake() {
        Self = this;
    }
}
