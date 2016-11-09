using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private static GameController instance;

    private static string[] TRACKS = { "small", "medium", "large" };

	static public int playerAmount;
	static public int difficulty = 3;
	static public int track;
	public GameObject[] tracks;
	public Vector3[] cameraPosition;
	public Vector3[] cameraRotation;
	public Camera _camera;

    public EndGame endGamePanel;
	public PauseGame pausePanel;

    private bool _isGamePaused;
    private bool _isGameFinished;

    public static GameController getInstance() {
        return instance;
    }

    private void Awake() {
        instance = this;
		startGame();
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
		for (int i = 0; i < tracks.Length; i++) {
			tracks [i].SetActive (track == i + 1);
		}
        endGamePanel.gameObject.SetActive(false);
		_camera.transform.position = cameraPosition [track-1];
		_camera.transform.eulerAngles = cameraRotation[track-1];
    }

    public void finishGame(int winnerPlayer, float bestLap) {
        _isGameFinished = true;
        endGamePanel.gameObject.SetActive(true);
        endGamePanel.playerWon(winnerPlayer, bestLap);
		if(GameController.playerAmount == 2 || winnerPlayer == 1){
			updateBestLap(bestLap);
		}
    }

    private void updateBestLap(float bestLap) {
        var trackString = TRACKS[track - 1];
        var bestLapString = "best_lap_" + trackString;
        float previousBestLap = PlayerPrefs.GetFloat(bestLapString, float.MaxValue);
        PlayerPrefs.SetFloat(bestLapString, Mathf.Min(bestLap, previousBestLap));
    }

}
