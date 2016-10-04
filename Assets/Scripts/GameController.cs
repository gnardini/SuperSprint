using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController instance;

    private bool _isGamePaused;

    public static GameController getInstance() {
        return instance;
    }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad( this.gameObject );
    }

    public bool isPaused() {
        return _isGamePaused;
    }

    public void togglePause() {
        _isGamePaused = !_isGamePaused;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.P)) {
            togglePause();
        }
    }

}
