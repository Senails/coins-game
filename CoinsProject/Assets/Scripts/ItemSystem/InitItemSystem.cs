using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitItemSystem : MonoBehaviour
{
    public GameObject bank;
    public GameObject inventory;
    public GameObject dropZone;
    // Start is called before the first frame update
    void Start()
    {
        BankWindow bankScript = bank.GetComponent<BankWindow>();
        Inventory inventoryScript = inventory.GetComponent<Inventory>();
        DropableZone dropZoneScript = dropZone.GetComponent<DropableZone>();

        BankWindow.Self=bankScript;
        Inventory.Self=inventoryScript;
        DropableZone.Self=dropZoneScript;
    }
}
