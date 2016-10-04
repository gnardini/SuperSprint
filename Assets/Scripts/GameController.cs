using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController instance;

	static public int playerAmount;

    public EndGame endGamePanel;
	public PauseGame pausePanel;

    private bool _isGamePaused;
    private bool _isGameFinished;

    public static GameController getInstance() {
        return instance;
    }

    private void Awake() {
        instance = this;
    }

    void Update() {
		if (Input.GetKeyUp(KeyCode.Escape)) {
            togglePause();
        }
    }

    public bool isPaused() {
        return _isGamePaused || _isGameFinished;
    }

    public void togglePause() {
        _isGamePaused = !_isGamePaused;
		pausePanel.gameObject.SetActive(_isGamePaused);
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
