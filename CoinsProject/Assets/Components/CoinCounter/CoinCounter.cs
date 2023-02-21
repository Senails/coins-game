using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        placeOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void placeOnStart(){
        Utils.setPositionRight(transform,20);
        Utils.setPositionTop(transform,20);

        Utils.showComponent(transform);
    }
}
