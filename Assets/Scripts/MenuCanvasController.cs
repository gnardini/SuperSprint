using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour {

	public GameObject menu;
	public GameObject difficultyMenu;
	public GameObject helpMenu;
	public GameObject trackMenu;
	public Text bestLapText;

	void Start(){
		float bestSmallLap = PlayerPrefs.GetFloat("best_lap_small", -1f);
        float bestMediumLap = PlayerPrefs.GetFloat("best_lap_medium", -1f);
        float bestLargeLap = PlayerPrefs.GetFloat("best_lap_large", -1f);
		bestLapText.text = 
            StringUtils.floatToTime(bestSmallLap) +
            "\n" + StringUtils.floatToTime(bestMediumLap) +
            "\n" + StringUtils.floatToTime(bestLargeLap);
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

	private void activeTrackMenu(bool b){
		trackMenu.SetActive (b);
	}

	public void enableMenu(){
		activeMenu (true);
		activeDifficulyMenu (false);
		activeHelpMenu (false);
		activeTrackMenu (false);
	}

	public void enableDifficultyMenu(){
		activeDifficulyMenu (true);
		activeMenu (false);
		activeHelpMenu (false);
		activeTrackMenu (false);
	}

	public void enableHelp(){
		activeHelpMenu (true);
		activeMenu (false);
		activeDifficulyMenu (false);
		activeTrackMenu (false);
	}

	public void enableTrackMenu(){
		activeHelpMenu (false);
		activeMenu (false);
		activeDifficulyMenu (false);
		activeTrackMenu (true);
	}

}
