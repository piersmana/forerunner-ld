using UnityEngine;
using System.Collections;

[System.Serializable]
public class Environment {
	
	public Texture2D background;
	public Texture2D ground;
	public Texture2D left;
	public Texture2D right;
	public Color transitionColor;
	public Color sunBrightness;
	public bool environmentHeight;
	
	public Environment() {
	}
}

public class EnvironmentControl : MonoBehaviour {
	
	public Environment[] environments = new Environment[2];
	
	public static EnvironmentControl Instance;
	
	private int activeEnvironment;
	
	private static Renderer background;
	private static Renderer ground;
	private static Renderer leftside;
	private static Renderer rightside;
	
	private static CameraControl cam;
	private static PlayerMovementControl playermove;
	private static PlayerMovementInput playermoveinput;
	private static PlayerDiveInput playerdive;
	private static Light sun;
	
	void Awake() {
		
		Instance = this;
		Instance.activeEnvironment = 0;
		
		ParallaxControl[] sceneObjects = GameObject.Find("_World").GetComponentsInChildren<ParallaxControl>(true);
		
		foreach (ParallaxControl o in sceneObjects) {
			switch (o.gameObject.name) {
			case "Background":
				background = o.renderer;
				break;
			case "Ground":
				ground = o.renderer;
				break;
			case "Left":
				leftside = o.renderer;
				break;
			case "Right":
				rightside = o.renderer;
				break;
			}
		}
		
		cam = GameObject.FindObjectOfType<CameraControl>();
		playerdive = GameObject.FindObjectOfType<PlayerDiveInput>();
		playermove = GameObject.FindObjectOfType<PlayerMovementControl>();
		playermoveinput = GameObject.FindObjectOfType<PlayerMovementInput>();
		sun = GameObject.Find ("_Sun").light;
	}
	
	public static void Reset() {
		Instance.activeEnvironment = -1;
		ShiftEnvironment();
	}
	
	public static void ShiftEnvironment() {
		Instance.activeEnvironment = (Instance.activeEnvironment + 1) % Instance.environments.Length;
		Instance.StartCoroutine(TransitionEnvironment(Instance.environments[Instance.activeEnvironment]));
	}

	public static void Nighttime() {
		Instance.StartCoroutine(StartNight());
	}

	static IEnumerator StartNight() {
		Color nightHue = Color.red;
		Material mb = background.material;
		Material mf = ground.material;

		while (mb.color != nightHue) {
			mb.color = (Color)Vector4.MoveTowards(mb.color,nightHue, Time.deltaTime);
			mf.color = (Color)Vector4.MoveTowards(mf.color,nightHue, Time.deltaTime);
			yield return null;
		}
	}

	static IEnumerator TransitionEnvironment(Environment to) {
		playermoveinput.enabled = false;
		playerdive.enabled = false;
		playermove.MovePlayer(1,1,1);
		if (to.environmentHeight) {
			cam.LookUp(40f);
			MusicControl.PlayDay();
		}
		else {
			cam.LookDown(40f);
			MusicControl.PlayNight();
		}

		ground.GetComponent<ParallaxControl>().ZoomIn(5f);

		yield return new WaitForSeconds(1.5f);

		playermove.MovePlayer(1,0,4);
		
		cam.FadeToColor(to.transitionColor,5f);
		
		yield return new WaitForSeconds(.5f);
		
		cam.LookLevel(40f);

		yield return new WaitForSeconds(1f);

		playermove.MovePlayer(1,1,1);
		
		if (to.background != null) {
			background.gameObject.SetActive(true);
			background.material.mainTexture = to.background;
			background.material.color = Color.white;
		}
		else {
			background.gameObject.SetActive(false);
		}
		if (to.ground != null) {
			ground.gameObject.SetActive(true);
			ground.material.mainTexture = to.ground;
			ground.material.color = Color.white;
		}
		else {
			ground.gameObject.SetActive(false);
		}
		if (to.left != null) {
			leftside.gameObject.SetActive(true);
			leftside.material.mainTexture = to.left;
		}
		else {
			leftside.gameObject.SetActive(false);
		}
		if (to.right != null) {
			rightside.gameObject.SetActive(true);
			rightside.material.mainTexture = to.right;
		}
		else {
			rightside.gameObject.SetActive(false);
		}
		
		ground.GetComponent<ParallaxControl>().ZoomOut(5f);

		sun.color = to.sunBrightness;
		
		cam.FadeToColor(Color.clear,.5f);
		
		yield return new WaitForSeconds(1.5f);
	}
}
