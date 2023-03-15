using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitItemSystem : MonoBehaviour
{
    public GameObject bank;
    public GameObject inventory;
    public GameObject dropZone;
    public GameObject choiceQuantity;
    // Start is called before the first frame update
    void Start()
    {
        BankWindow bankScript = bank.GetComponent<BankWindow>();
        Inventory inventoryScript = inventory.GetComponent<Inventory>();
        DropableZone dropZoneScript = dropZone.GetComponent<DropableZone>();
        ChoiceQuantity choiceQuantityScript = choiceQuantity.GetComponent<ChoiceQuantity>();

        BankWindow.Self=bankScript;
        Inventory.Self=inventoryScript;
        DropableZone.Self=dropZoneScript;
        ChoiceQuantity.Self=choiceQuantityScript;
    }
}
