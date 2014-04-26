using UnityEngine;
using System.Collections;

[System.Serializable]
public class Environment {

	public GameObject activeSet;
	public Texture activeTexture1;
	public Texture activeTexture2;
	public Color transitionColor;
	public bool environmentHeight;


	public Environment() {
	}
}

public class EnvironmentControl : MonoBehaviour {

	public Environment[] environments = new Environment[2];

	public static EnvironmentControl Instance;

	private int activeEnvironment;
	
	private static CameraControl cam;
	private static PlayerMovementControl playermove;
	private static PlayerMovementInput playermoveinput;
	private static PlayerDiveInput playerdive;

	void Awake() {
		Instance = this;
		Instance.activeEnvironment = 0;
		
		cam = GameObject.FindObjectOfType<CameraControl>();
		playerdive = GameObject.FindObjectOfType<PlayerDiveInput>();
		playermove = GameObject.FindObjectOfType<PlayerMovementControl>();
		playermoveinput = GameObject.FindObjectOfType<PlayerMovementInput>();
	}

	void Start() {
	}

	public static void ShiftEnvironment() {
		Instance.StartCoroutine(TransitionEnvironment(Instance.environments[Instance.activeEnvironment], Instance.environments[(Instance.activeEnvironment + 1) % Instance.environments.Length]));
		Instance.activeEnvironment = (Instance.activeEnvironment + 1) % Instance.environments.Length;
	}

	static IEnumerator TransitionEnvironment(Environment from, Environment to) {
		playermoveinput.enabled = false;
		playerdive.enabled = false;
		playermove.TriggerPlayerMovement(1,1,1);
		if (to.environmentHeight) 
			cam.LookUp(25f);
		else
			cam.LookDown(25f);

		yield return new WaitForSeconds(1.5f);

		cam.FadeToColor(to.transitionColor,5f);
		
		yield return new WaitForSeconds(.5f);

		cam.LookLevel(20f);

		yield return new WaitForSeconds(1f);

		from.activeSet.SetActive(false);
		to.activeSet.SetActive(true);
		cam.FadeToColor(Color.clear,.5f);

		yield return new WaitForSeconds(1.5f);

		playermoveinput.enabled = true;
		playerdive.enabled = true;
	}
}
