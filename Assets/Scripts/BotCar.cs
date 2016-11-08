using UnityEngine;
using System.Collections;

public class BotCar : Car {

	private int index = 1;
	private double breakFactor;
	private int steps;
	private int radius;

	private Vector3[] points;

	private int i = 0;
	public bool isBot;

	void Update(){
		if (!isBot) {
			base.Update ();
			return;
		}
        if (checkPaused()) {
			return;
		}
		Vector3 sub = points [index] - transform.position;
		if (sub.magnitude < radius) {
			Debug.Log ("reached "+points [index]);
            index = (index + 1) % points.Length;
			Debug.Log ("index "+index+" "+points [index]);
		} else {
			Vector3 dir = -transform.forward;
			sub /= sub.magnitude;
			dir /= dir.magnitude;
			float vectorProd = sub.x * dir.z - sub.z * dir.x;
			if (vectorProd < 0) {
				rotateLeft ();
			}
			if (vectorProd > 0) {
				rotateRight ();
			}
			if ((Mathf.Abs (vectorProd) < breakFactor) || (i++ % steps == 0)) {
				speedUp ();
			}
			fixedPosition ();
		}
	}

	private void fillPoints(){
		GameObject track = GameObject.Find(string.Format("Track{0}", GameController.track));
		points = new Vector3[track.transform.childCount - 3];
		int[] forwardSize = { 10, 15, 25 };
		Debug.Log (track.transform.childCount);
		int forward = forwardSize[GameController.track-1];			
		for (int i = 0; i <= forward; i++) {
			points [i] = track.transform.GetChild(i+3).position;
		}
		for (int i = points.Length+2, k = forward+1; k < points.Length; i--, k++) {
			points [k] = track.transform.GetChild(i).position;
		}
	}

	private void duplicatePoints(){
		Vector3[] points2 = new Vector3[points.Length * 2];
		for (int i = 0; i < points.Length; i++) {
			points2 [2 * i] = points [i];
			points2 [2 * i + 1] = (points [i] + points [(i + 1)%points.Length]) / 2;
		}
	}


	protected override Player initPlayer2(){
		isBot = GameController.playerAmount == 1;
		fillPoints ();
		if (isBot) {
			radius = 10;
			switch (GameController.difficulty) {
				case 1:
					breakFactor = 0.1f;
					steps = 3;
					radius = 20;
					break;
				case 2:
					breakFactor = 0.3f;
					steps = 2;
					radius = 20;
					break;
				case 3:
					duplicatePoints ();
					breakFactor = 0.5f;
					steps = 2;
					radius = 24;
					break;
			}
		}
		return base.initPlayer2 ();
	}
}

