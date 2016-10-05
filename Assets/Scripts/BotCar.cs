using UnityEngine;
using System.Collections;

public class BotCar : Car {

	private int index = 0;
	private double breakFactor;
	private int steps;
	private int radius;

	private Vector3[] points;

	private Vector3[] points2 = {
		new Vector3(68.8f,1.8f, 7.7f),
		new Vector3(80.7f,1.8f, 21.0f),
		new Vector3(75.9f,1.8f, 48.8f),
		new Vector3(1.9f,1.8f, 64.3f),
		new Vector3(-11.5f,1.7f, 102.4f),
		new Vector3(69.1f,1.8f, 127.2f),
		new Vector3(81.3f,1.8f, 165.9f),
		new Vector3(54.1f,1.8f, 179.1f),
		new Vector3(-50.3f,1.8f, 177.5f),
		new Vector3(-62.1f,1.7f, 173.9f),
		new Vector3(-73.2f,1.8f, 25.3f),
		new Vector3(-68.0f,1.8f, 13.4f),
	};




	private Vector3[] points3 = {
		new Vector3(38.1f,1.8f, -8.8f), 
		new Vector3(63.9f,1.8f, -4.9f), 
		new Vector3(81.7f,1.8f, 6.5f), 
		new Vector3(91.9f,1.8f, 25.8f), 
		new Vector3(91.8f,1.8f, 39.6f), 
		new Vector3(82.2f,1.8f, 58.7f), 
		new Vector3(58.8f,1.8f, 68.4f), 
		new Vector3(34.8f,1.8f, 62.4f), 
		new Vector3(17.1f,1.8f, 55.1f), 
		new Vector3(-12.4f,1.8f, 80.2f), 
		new Vector3(-16.8f,1.8f, 100.2f), 
		new Vector3(-6.6f,1.8f, 119.3f), 
		new Vector3(22.6f,1.8f, 128.9f), 
		new Vector3(41.7f,1.8f, 122.9f), 
		new Vector3(55.5f,1.8f, 118.4f), 
		new Vector3(72.6f,1.8f, 121.8f), 
		new Vector3(88.0f,1.8f, 141.4f), 
		new Vector3(91.3f,1.7f, 164.5f), 
		new Vector3(80.1f,1.8f, 181.4f), 
		new Vector3(62.5f,1.8f, 190.4f), 
		new Vector3(-48.0f,1.8f, 190.1f), 
		new Vector3(-73.0f,1.8f, 175.5f), 
		new Vector3(-86.1f,1.8f, 148.0f), 
		new Vector3(-85.3f,1.8f, 23.8f), 
		new Vector3(-70.2f,1.8f, 0.3f), 
		new Vector3(-31.4f,1.8f, -9.4f), 
		new Vector3(1.8f,1.8f, -9.4f), 
	};

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
			//Debug.Log (points [index]);
            index = (index + 1) % points.Length;
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


	protected override Player initPlayer2(){
		isBot = GameController.playerAmount == 1;
		if (isBot) {
			radius = 10;
			switch (GameController.difficulty) {
				case 1:
					breakFactor = 0.1f;
					steps = 3;
					points = points3;
					break;
				case 2:
					breakFactor = 0.3f;
					steps = 2;
					points = points3;
					break;
				case 3:
					breakFactor = 0.8f;
					steps = 2;
					points = points3;
					break;
				case 4:
					breakFactor = 1f;
					steps = 2;
					radius = 12;
					points = points2;
					break;
			}
		}
		return base.initPlayer2 ();
	}
}

