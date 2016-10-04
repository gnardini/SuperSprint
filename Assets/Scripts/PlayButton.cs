using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {

    public Text bestLapText;
	public MenuCanvasController controller;

    void Start() {
        float bestLap = PlayerPrefs.GetFloat("best_lap", -1f);
        bestLapText.text = "Mejor Vuelta: " + StringUtils.floatToTime(bestLap);
    }

    public void PlayAlone() {
		controller.disableMenu ();
		controller.enableDifficultyMenu ();
    }

	public void PlayAloneEasy(){
		GameController.playerAmount = 1;
		GameController.difficulty = 1;
		SceneManager.LoadSceneAsync("Race");	
	}

	public void PlayAloneMedium(){
		GameController.playerAmount = 1;
		GameController.difficulty = 2;
		SceneManager.LoadSceneAsync("Race");	
	}

	public void PlayAloneHard(){
		GameController.playerAmount = 1;
		GameController.difficulty = 3;
		SceneManager.LoadSceneAsync("Race");	
	}

	public void Back(){
		controller.disableDifficultyMenu ();
		controller.enableMenu ();
	}

	public void Multiplayer(){
		GameController.playerAmount = 2;
		SceneManager.LoadSceneAsync("Race");
	}

}
