using UnityEngine;
using UnityEngine.SceneManagement;

public class DangeonGate : MonoBehaviour
{
    public int LocaiondID = 0;
    public int SceneID = 0;
    private void OnTriggerEnter2D(Collider2D other) {
        SaveMeneger.Self.LoadingScene(SceneID);
        // SceneManager.LoadScene(SceneID);
    }
}
