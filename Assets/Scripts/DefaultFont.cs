using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DefaultFont : MonoBehaviour {

	void Start () {
        Font defaultFont = Resources.Load("Roboto-Medium") as Font;
        foreach (Text text in GetComponentsInChildren<Text>()) {
            text.font = defaultFont;
        }
	}

}
