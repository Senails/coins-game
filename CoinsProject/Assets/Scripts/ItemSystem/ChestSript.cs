using UnityEngine;

public class ChestSript : MonoBehaviour
{
    public string BankName;
    public int MaxMass;
    public int SlotsCount;


    private bool connect = false;
    private ChestR _chestEntiti;

    
    private void Start() {
        _chestEntiti = new ChestR(BankName,MaxMass,SlotsCount);
    }
    private void Update() {
        if (!connect) return;
        if (!Input.GetKeyDown(OptionsManager.Config.KyeDictionary["Взаимодействие"])) return;
        ItemManager.Self.TogleChestWindow(_chestEntiti);
    }


    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag != "Player") return;
        HintManager.Connect();
        this.connect = true;
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.tag != "Player") return;
        HintManager.Disconnect();
        this.connect = false;
        ItemManager.Self.CloseChestWindow();
        ItemManager.Self.CloseChoiseWindow();
    }
}
