using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartedNet : MonoBehaviour
{
    bool flag = true;
    void Start()
    {
        TestNetwork.connect();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Z)){
            if (!flag) return;
            TestNetwork.senMessage("белеберда");
            flag=false;
        }else{
            flag=true;
        }
    }
}
