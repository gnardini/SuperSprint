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
		totalLaps = 2;
		currentLaps = 2;
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

	private string floatToTime(float secs){
		if (secs < 0) {
			return "--:--:--";
		}
		int min = (int)(secs / 60);
		int seg = (int)(secs - min * 60);
		int ms = (int)((secs - min * 60 - seg) * 100);
		return min.ToString("D2") + ":" + seg.ToString("D2") + ":" + ms.ToString("D2");
	}

	private void updateTimerText(){
		timerText.text = "Tiempo: " + floatToTime (timer);
	}

	private void updateLapsText(){
		lapsToWinText.text = "Vueltas " + (totalLaps - currentLaps) + "/" + totalLaps;
	}

	private void updateBestText (){
		bestLapText.text = "Mejor tiempo: " + floatToTime (bestTime);
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
}
