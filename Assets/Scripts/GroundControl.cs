using UnityEngine;
using System.Collections;

public class GroundControl : MonoBehaviour {

	public float scrollSpeed = 5f;

	private Material m;
	private Vector2 origTiling;

	private bool toggle = true;

	private Transform gameCamera;

	void Awake() {
		m = renderer.material;
		origTiling = m.mainTextureScale;

		gameCamera = GameObject.Find("_Game Camera Rig").transform;
	}

	//Testing
	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (toggle) 
				StartDescend(10);
			else 
				StartAscend(10);
			
			toggle = !toggle;
		}
	}

	void LateUpdate () {
		m.mainTextureOffset = new Vector2(-m.mainTextureScale.x / 2, (m.mainTextureOffset.y + scrollSpeed *  Time.deltaTime) );
	}
	
	public void StartAscend(float rate) {
		StopAllCoroutines();
		StartCoroutine("Ascend", rate);
	}
	
	IEnumerator Ascend(float rate) {
		while (m.mainTextureScale != origTiling) {
			
			m.mainTextureScale = Vector2.MoveTowards(m.mainTextureScale, origTiling, rate * Time.deltaTime);

			gameCamera.rotation = Quaternion.RotateTowards(gameCamera.rotation, Quaternion.identity, 2 * rate * Time.deltaTime);
			
			yield return null;
		}
	}

	public void StartDescend(float rate) {
		StopAllCoroutines();
		StartCoroutine("Descend", rate);
	}
	
	IEnumerator Descend(float rate) {
		while (m.mainTextureScale != Vector2.one) {
			
			m.mainTextureScale = Vector2.MoveTowards(m.mainTextureScale, Vector2.one, rate * Time.deltaTime);

			gameCamera.rotation = Quaternion.RotateTowards(gameCamera.rotation, Quaternion.Euler(70,0,0), 2 * rate * Time.deltaTime);
			
			yield return null;
		}
	}
}
