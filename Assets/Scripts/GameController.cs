using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public EndGame endGamePanel;

    private bool _isGamePaused;
    private bool _isGameFinished;

    public static GameController getInstance() {
        return instance;
    }

    private void Awake() {
        if (instance != null && instance != this) {
            instance.startGame();
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad( this.gameObject );
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.P)) {
            togglePause();
        }
    }

    public bool isPaused() {
        return _isGamePaused || _isGameFinished;
    }

    public void togglePause() {
        _isGamePaused = !_isGamePaused;
    }

    public void startGame() {
        _isGameFinished = false;
        endGamePanel.gameObject.SetActive(false);
    }

    public void finishGame(int winnerPlayer, float bestLap) {
        _isGameFinished = true;
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.playerWon(winnerPlayer, bestLap);
        updateBestLap(bestLap);
    }

    private void updateBestLap(float bestLap) {
        float previousBestLap = PlayerPrefs.GetFloat("best_lap", float.MaxValue);
        PlayerPrefs.SetFloat("best_lap", Mathf.Min(bestLap, previousBestLap));
    }

}
