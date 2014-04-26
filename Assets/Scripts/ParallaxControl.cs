using UnityEngine;
using System.Collections;

public class ParallaxControl : MonoBehaviour {

	public Vector2 scrollSpeed = new Vector2(0,5f);
	public float zoomSpeed = 10f;

	private Material m;
	private Vector2 origTiling;

	private Vector2 target;

	void Awake() {
		m = renderer.material;
		origTiling = m.mainTextureScale;
		target = origTiling;
	}

	void Update() {
		m.mainTextureScale = Vector2.MoveTowards(m.mainTextureScale, target, zoomSpeed * Time.deltaTime);
		m.mainTextureOffset = m.mainTextureOffset + scrollSpeed * Time.deltaTime - m.mainTextureScale / 2;
	}
	
	public void ZoomOut(float rate) {
		target = origTiling;
		zoomSpeed = rate;
	}

	public void ZoomIn(float rate) {
		target = Vector2.one;
		zoomSpeed = rate;
	}
}
