using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static void LootSmallCoins(){
        ScoreMeneger.AddCoins(1);
    }
    public static void LootLargeCoins(){
        ScoreMeneger.AddCoins(2);
    }

}
