using UnityEngine;
using System.Collections;

public class BotCar : Car {

	private int index = 0;
	private Vector3[] points = {
		new Vector3(10f,2.3f,-6.3f),
		new Vector3(40f,2.3f,-6.3f),
		new Vector3(65f,2.3f,0.0f),
		new Vector3(80f,2.3f,20.0f),
		new Vector3(80f,2.3f,40.0f),
		new Vector3(70f,2.3f,60.0f),
		new Vector3(6f,2.3f,65.0f),
		new Vector3(-7f,2.3f,77.0f),
		new Vector3(-7f,2.3f,94.0f),
		new Vector3(3f,2.3f,115.0f),
        new Vector3(73f,2.3f,120.0f),
        new Vector3(87f,2.3f,140.0f),
        new Vector3(86f,2.3f,154.0f),
        new Vector3(81f,2.3f,170.0f),
        new Vector3(59f,2.3f,180.0f),
        new Vector3(-58f,2.3f,177.0f),
        new Vector3(-68f,2.3f,169.0f),
        new Vector3(-77f,2.3f,161.0f),
        new Vector3(-74f,2.3f,21.0f),
        new Vector3(-65f,2.3f,9.0f),
        new Vector3(-52f,2.3f,3.0f),
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
		if (sub.magnitude < 5) {
            index = (index + 1) % points.Length;
		} else {
			Vector3 dir = -transform.forward;
			double vectorProd = sub.x * dir.z - sub.z * dir.x;
			if (vectorProd < 0) {
				rotateLeft ();
			}
			if (vectorProd > 0) {
				rotateRight ();
			}
			if (i++ % 3 == 0) {
				speedDown ();
			} else {
				speedUp ();
			}
			fixedPosition ();
		}
	}


	protected override Player initPlayer2(){
		Debug.Log ("LLamame");
		isBot = GameController.playerAmount == 1;
		return base.initPlayer2 ();
	}
}
