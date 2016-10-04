using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController instance;

    private bool _isGamePaused;

    public static GameController getInstance() {
        return instance;
    }

    void Start() {
        if (instance != null) {
            Destroy(instance);
        }
        instance = this;
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
