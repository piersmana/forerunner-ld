using UnityEngine;
using System.Collections;

[System.Serializable]
public class Level {
	public int countHazards;
	public float delayBetween;
	public int groupHazards;
	public Hazard[] availHazards;
}

public class GameManager : MonoBehaviour {

	public Level[] levels;

	private PlayerDiveInput p_dive;
	private PlayerMovementInput p_input;
	private PlayerMovementControl p_movement;
	private PlayerDamageControl p_damage;

	private CameraControl cam;

	void Awake () {
		p_dive = GameObject.FindObjectOfType<PlayerDiveInput>().GetComponent<PlayerDiveInput>();
		p_input = GameObject.FindObjectOfType<PlayerMovementInput>().GetComponent<PlayerMovementInput>();
		p_movement = GameObject.FindObjectOfType<PlayerMovementControl>().GetComponent<PlayerMovementControl>();
		p_damage = GameObject.FindObjectOfType<PlayerDamageControl>().GetComponent<PlayerDamageControl>();

		cam = GameObject.FindObjectOfType<CameraControl>().GetComponent<CameraControl>();
	}

	void Start() {
		p_damage.playerDamaged += CameraShake;
		StartCoroutine("StartGame");
	}

	IEnumerator StartGame() {
		for (int i = 0; i < levels.Length; i++) {
			while (true) { 
				for (int j = 0; j < levels[i].countHazards; j++) {
					yield return new WaitForSeconds(levels[i].delayBetween);
					for (int k = -1; k < levels[i].groupHazards; k++) {
						SpawnRandomHazard(ref levels[i].availHazards);
						yield return new WaitForSeconds(.3f);
					}
				}
				//break;
				yield return new WaitForSeconds(3f);
				p_dive.enabled = true;
				yield return new WaitForSeconds(4f);
				p_dive.enabled = false;
				break;
			}
		}
	}

	void SpawnRandomHazard(ref Hazard[] hazards) {
		Instantiate(hazards[Random.Range(0,hazards.Length)].gameObject);
	}

	void CameraShake() {
		cam.FadeToColor(Color.red,8f);
		StartCoroutine("Shake");
	}

	IEnumerator Shake() {
		int shakeCount = 3;
		Vector3 startpos = cam.transform.position;
		while (shakeCount > 0) {
			cam.transform.position += new Vector3(0,.2f);
			yield return new WaitForSeconds(.05f);
			cam.transform.position += new Vector3(0,-.2f);
			yield return new WaitForSeconds(.05f);
			cam.FadeToColor(Color.clear,1f);
			shakeCount --;
		}
		cam.transform.position = startpos;
	}
}
