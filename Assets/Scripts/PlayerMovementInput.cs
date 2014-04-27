using UnityEngine;
using System.Collections;

public class PlayerMovementInput : MonoBehaviour {

	private PlayerMovementControl movement;

	void Awake() {
		movement = GetComponent<PlayerMovementControl>();
	}

	// Update is called once per frame
	void Update () {

		movement.MovePlayer(1 + (int)Input.GetAxis("Horizontal"), 1 - (int)Input.GetAxis("Vertical"));

	}
}
