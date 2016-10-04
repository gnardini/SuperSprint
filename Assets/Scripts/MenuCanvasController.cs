using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour {

	public PlayButton playAlone;
	public PlayButton multiplayer;
	public PlayButton easy;
	public PlayButton medium;
	public PlayButton hard;
	public PlayButton back;
	public Text bestLap;

	private void activeDifficulyMenu(bool b){
		hard.gameObject.SetActive (b);
		medium.gameObject.SetActive (b);
		easy.gameObject.SetActive (b);
		back.gameObject.SetActive (b);
	}

	private void activeMenu(bool b){
		playAlone.gameObject.SetActive (b);
		multiplayer.gameObject.SetActive (b);
		bestLap.gameObject.SetActive (b);
	}

	public void disableMenu(){
		activeMenu (false);
	}

	public void enableMenu(){
		activeMenu (true);
	}

	public void disableDifficultyMenu(){
		activeDifficulyMenu (false);
	}

	public void enableDifficultyMenu(){
		activeDifficulyMenu (true);
	}


}
