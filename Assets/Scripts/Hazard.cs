using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class Hazard : MonoBehaviour {

	public Vector3 velocity = -Vector3.forward * 10;
	public Vector3 angularVelocity = Vector3.zero;
	public Vector3 startPos = new Vector3(0,0,60);
	public Vector3 startRot = Vector3.zero;
	public bool fixedPlacement = true;
	public bool fixedRotation = true;

	private Transform t;
	private Rigidbody r;

	void Awake() {
		t = transform;
		r = rigidbody;
	}

	void Start() {
		if (!fixedPlacement) 
			t.position = GameObject.FindGameObjectWithTag("Player").transform.position + new Vector3(Mathf.Clamp(Random.Range(-2f,2f),-6,6), Mathf.Clamp(Random.Range(-2f,2f),-6,6), startPos.z);
		else
			t.position = startPos;
		
		if (!fixedRotation) 
			t.rotation = Quaternion.Euler(startRot.x, startRot.y, Random.Range(-90f,90f));
		else
			t.rotation = Quaternion.Euler(startRot);

		r.angularVelocity = angularVelocity * (Random.Range(0,2) == 1 ? 1 : -1);
		r.velocity = velocity;
	}

	void Update() {
		if (t.position.z < -20) {
			Destroy(gameObject);
		}
	}
}
