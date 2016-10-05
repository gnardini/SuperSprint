using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultFont : MonoBehaviour {

	public Font fontToLoad;

	void Start () {
        foreach (Text text in GetComponentsInChildren<Text>()) {
			text.font = fontToLoad;
        }
	}

}
