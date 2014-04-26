using UnityEngine;
using System.Collections;

public class PlayerDiveInput : MonoBehaviour {

	private PlayerMovementControl movement;
	private PlayerMovementInput input;

	void Awake() {
		movement = GetComponent<PlayerMovementControl>();
		input = GetComponent<PlayerMovementInput>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			input.enabled = false;
			movement.TriggerPlayerMovement(1,1,1);
			//begin loading next environment
		}
	}

}
