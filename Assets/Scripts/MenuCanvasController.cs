using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour {

	public GameObject menu;
	public GameObject difficultyMenu;
	public GameObject helpMenu;
	public Text bestLapText;

	void Start(){
		float bestLap = PlayerPrefs.GetFloat("best_lap", -1f);
		bestLapText.text = "Mejor Vuelta: " + StringUtils.floatToTime(bestLap);
	}

	private void activeHelpMenu(bool b){
		helpMenu.gameObject.SetActive (b);
	}

	private void activeDifficulyMenu(bool b){
		difficultyMenu.SetActive (b);
	}

	private void activeMenu(bool b){
		menu.SetActive (b);
	}

	public void enableMenu(){
		activeMenu (true);
		activeDifficulyMenu (false);
		activeHelpMenu (false);
	}

	public void enableDifficultyMenu(){
		activeDifficulyMenu (true);
		activeMenu (false);
		activeHelpMenu (false);
	}

	public void enableHelp(){
		activeHelpMenu (true);
		activeMenu (false);
		activeDifficulyMenu (false);
	}

}
