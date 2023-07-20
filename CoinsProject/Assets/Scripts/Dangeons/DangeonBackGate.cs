using UnityEngine;
using UnityEngine.SceneManagement;

public class DangeonBackGate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        SaveMeneger.Self.LoadingScene(1);
        // SceneManager.LoadScene(1);
    }
}
