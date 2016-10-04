using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayButton : MonoBehaviour {

    public Text bestLapText;

    void Start() {
        float bestLap = PlayerPrefs.GetFloat("best_lap", -1f);
        bestLapText.text = "Mejor Vuelta: " + StringUtils.floatToTime(bestLap);
    }

    public void PlayGame() {
        SceneManager.LoadSceneAsync("Race");
    }

}
