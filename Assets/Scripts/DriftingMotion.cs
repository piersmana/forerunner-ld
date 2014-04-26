using UnityEngine;
using System.Collections;

public class DriftingMotion : MonoBehaviour {

	public float driftDistance = .2f;

	public float driftSpeed = .4f;

	public float driftFrequency = 2f;

	private Transform t;
	private Transform parent;

	private float current;

	void Awake() {
		t = transform;
		parent = t.root;
		current = 0;
	}

	void Start() {
		InvokeRepeating("DriftOcillation",0,driftFrequency);
	}

	void Update () {
		t.position = parent.position + new Vector3(0,(current = Mathf.MoveTowards(current, driftDistance, driftSpeed * Time.deltaTime)),0);
	}

	void DriftOcillation() {
		driftDistance = -driftDistance;
	}

}
