using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {
	
	public MenuCanvasController controller;

    public void PlayAlone() {
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

	public void PlayAloneVeryHard(){
		GameController.playerAmount = 1;
		GameController.difficulty = 4;
		SceneManager.LoadSceneAsync("Race");	
	}

	public void Back(){
		controller.enableMenu ();
	}

	public void Multiplayer(){
		GameController.playerAmount = 2;
		SceneManager.LoadSceneAsync("Race");
	}

	public void Help(){
		controller.enableHelp ();
	}
}
