using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour {


	public void Continue(){
		GameController.getInstance ().togglePause ();
	}

	public void GoToMenu() {
		SceneManager.LoadSceneAsync("Menu");
	}
}
