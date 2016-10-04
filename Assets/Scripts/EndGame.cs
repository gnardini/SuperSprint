using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGame : MonoBehaviour {

    public Text _winnerText;
    public Text _bestLapText;

    public void playerWon(int playerNumber, float bestLap) {
        _winnerText.text = "Gana el Jugador " + playerNumber;
        _bestLapText.text = "Mejor vuelta: " + StringUtils.floatToTime(bestLap);
    }

    public void GoToMenu() {
        SceneManager.LoadSceneAsync("Menu");
    }
}
