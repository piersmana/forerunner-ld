using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Transform t;

	private Quaternion target = Quaternion.identity;
	private float speed = 4f;

	void Awake() {
		t = transform;
	}

	public void ResetView() {
		t.rotation = Quaternion.identity;
	}

	public void LookLevel() {
		target = Quaternion.identity;
	}

	public void LookUp() {
		target = Quaternion.Euler(-70,0,0);
	}
	public void LookDown() {
		target = Quaternion.Euler(70,0,0);
	}

	void LateUpdate() {
		t.rotation = Quaternion.RotateTowards(t.rotation, target, speed * Time.deltaTime);
	}
}
