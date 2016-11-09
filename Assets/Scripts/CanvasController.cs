using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour {

	public Text lapsToWinText;
	public Text timerText;
	public Text bestLapText;

    private GameController _gameController;
	private float timer;
	private float bestTime;
	private int totalLaps;
	private int currentLaps;

	// Use this for initialization
	void Start () {
		totalLaps = 3;
		currentLaps = 1;
		bestTime = -1f;
		timer = 0f;
		updateLapsText ();
		updateBestText ();
        _gameController = GameController.getInstance();
	}
	
	// Update is called once per frame
	void Update () {
        if (_gameController.isPaused()) {
            return;
        }
		timer += Time.deltaTime;
		updateTimerText ();
	}

	private void updateTimerText(){
		timerText.text = "Tiempo: " + StringUtils.floatToTime (timer);
	}

	private void updateLapsText(){
		lapsToWinText.text = "Vueltas " + (totalLaps - currentLaps) + "/" + totalLaps;
	}

	private void updateBestText (){
        bestLapText.text = "Mejor tiempo: " + StringUtils.floatToTime (bestTime);
	}

	private void maybeUpdateBestLapText(){
		if (bestTime == -1f || bestTime > timer) {
			bestTime = timer;
			updateBestText ();
			startFlickering ();
		}
		timer = 0f;
	}

	public void updateLaps(int laps){
		if (laps < currentLaps) {
			currentLaps = laps;
			updateLapsText ();
			maybeUpdateBestLapText ();
		}
	}

	private void startFlickering() {
        StartCoroutine(FilckerLights());
    }

    private IEnumerator FilckerLights() {
		for (int i = 0; i < 5; i++) {
			bestLapText.gameObject.SetActive (false);
			yield return new WaitForSeconds (0.2f);
			bestLapText.gameObject.SetActive (true);
			yield return new WaitForSeconds (0.2f);
		}
    }

    public float getBestLap() {
        return bestTime;
    }

}
