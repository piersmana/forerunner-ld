using UnityEngine;
using System.Collections;

public class PlayerDiveInput : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			EnvironmentControl.ShiftEnvironment();
		}
	}

}
