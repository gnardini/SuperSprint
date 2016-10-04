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
		new Vector3(40f,2.3f,70.0f),
		new Vector3(0f,2.3f,70.0f),
		new Vector3(-10f,2.3f,90.0f),
		new Vector3(-10f,2.3f,110.0f),
	};
	private int i = 0;

	void Update(){
		if (index >= points.Length) {
			return;
		}
		Debug.Log (transform.position);
		Vector3 sub = points [index] - transform.position;
		if (sub.magnitude < 5) {
			index = (index + 1);
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
}
