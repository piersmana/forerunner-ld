using UnityEngine;
using System.Collections;

public class ParallaxControl : MonoBehaviour {

	public Vector2 scrollSpeed = new Vector2(0,5f);
	public float zoomSpeed = 10f;

	private Material m;
	private Renderer r;
	private Vector2 origTiling;
	private Vector2 currentOffset;

	private Vector2 target;

	void Awake() {
		r = renderer;
		m = r.material;
		m.mainTextureOffset = currentOffset = Vector2.zero;
		target = origTiling = m.mainTextureScale;
	}

	void OnEnable() {
		m = r.material;
		currentOffset = Vector2.zero;
	}

	void Update() {
		m.mainTextureScale = Vector2.MoveTowards(m.mainTextureScale, target, zoomSpeed * Time.deltaTime);
		m.mainTextureOffset = (currentOffset += scrollSpeed * Time.deltaTime) - m.mainTextureScale / 2;
	}
	
	public void ZoomOut(float rate) {
		target = origTiling;
		zoomSpeed = rate;
	}

	public void ZoomIn(float rate) {
		target = Vector2.one / 5f;
		zoomSpeed = rate;
	}
}
