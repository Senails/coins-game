using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemTypes;

public class HealAction : ActionBase
{
    override public void Invoke(){
        Player.Self.AddHealth(30);
    }
}