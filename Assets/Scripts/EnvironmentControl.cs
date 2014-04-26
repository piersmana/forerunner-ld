using UnityEngine;
using System.Collections;

[System.Serializable]
public class Environment {

	public GameObject activeSet;
	public Texture activeTexture1;
	public Texture activeTexture2;
	public Color transitionColor;
	public bool environmentHeight;


	public Environment() {}

	public void EnterEnvironment() {
		activeSet.SetActive(true);
	}

	public void ExitEnvironment() {
		activeSet.SetActive(false);
	}
}

public class EnvironmentControl : MonoBehaviour {

	public Environment[] environments = new Environment[2];

	private int activeEnvironment;

	void Awake() {
		activeEnvironment = 0;
	}

	void Start() {
		environments[activeEnvironment].EnterEnvironment();
	}

	public void ShiftEnvironment() {
		environments[activeEnvironment].ExitEnvironment();
		activeEnvironment = (activeEnvironment + 1) % environments.Length;
		environments[activeEnvironment].EnterEnvironment();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
			ShiftEnvironment();
	}
}
