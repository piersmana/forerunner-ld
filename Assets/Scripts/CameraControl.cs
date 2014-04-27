using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private Transform t;

	private Quaternion rotTarget = Quaternion.identity;
	private float rotSpeed = 4f;

	private Color colorTarget = Color.clear;
	private float fadeSpeed = 10f;
	private Material fader;

	void Awake() {
		t = transform;
		fader = GameObject.Find("ScreenFade").renderer.material;
	}

	void Start() {
		ResetColor();
	}

	public void ResetView() {
		t.rotation = Quaternion.identity;
	}

	public void LookLevel(float speed) {
		rotTarget = Quaternion.identity;
		rotSpeed = speed;
	}

	public void LookUp(float speed) {
		rotTarget = Quaternion.Euler(-70,0,0);
		rotSpeed = speed;
	}

	public void LookDown(float speed) {
		rotTarget = Quaternion.Euler(70,0,0);
		rotSpeed = speed;
	}

	public void ResetColor() {
		fader.color = Color.clear;
	}

	public void FadeToColor(Color c, float speed) {
		colorTarget = c;
		fadeSpeed = speed;
	}

	void LateUpdate() {
		t.rotation = Quaternion.RotateTowards(t.rotation, rotTarget, rotSpeed * Time.deltaTime);
		fader.color = (Color)Vector4.MoveTowards(fader.color, colorTarget, fadeSpeed * Time.deltaTime);
	}
}
