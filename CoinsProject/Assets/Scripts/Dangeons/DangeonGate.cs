using UnityEngine;
using UnityEngine.SceneManagement;

public class DangeonGate : MonoBehaviour
{
    public int LocaiondID = 0;
    public int SceneID = 0;


    private bool _connected = false;


    private void Update() {
        if (!_connected) return;
        if (!Input.GetKeyDown(OptionsManager.Config.KyeDictionary["Взаимодействие"])) return;
        SaveMeneger.Self.LoadingScene(SceneID);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Player") return;
        HintManager.Connect();
        _connected = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag != "Player") return;
        HintManager.Disconnect();
        _connected = false;
    }
}
