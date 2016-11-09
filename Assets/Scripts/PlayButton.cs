using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {
	
	public MenuCanvasController controller;

	public void PlaySmallTrack(){
		GameController.track = 1;
		SceneManager.LoadSceneAsync ("Race");
	}

	public void PlayMediumTrack(){
		GameController.track = 2;
		SceneManager.LoadSceneAsync ("Race");
	}

	public void PlayLargeTrack(){
		GameController.track = 3;
		SceneManager.LoadSceneAsync ("Race");
	}

    public void PlayAlone() {
		controller.enableDifficultyMenu ();
    }

	public void PlayAloneEasy(){
		GameController.playerAmount = 1;
		GameController.difficulty = 1;
		TrackSelect ();
	}

	public void PlayAloneMedium(){
		GameController.playerAmount = 1;
		GameController.difficulty = 2;
		TrackSelect ();
	}

	public void PlayAloneHard(){
		GameController.playerAmount = 1;
		GameController.difficulty = 3;
		TrackSelect ();
	}

	public void TrackSelect (){
		controller.enableTrackMenu ();
	}
		

	public void Back(){
		controller.enableMenu ();
	}

	public void Multiplayer(){
		GameController.playerAmount = 2;
		TrackSelect ();
	}

	public void Help(){
		controller.enableHelp ();
	}
}
