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
		p_damage.playerDied += DeadEnd;
		StartCoroutine("OpeningSequence");
	}

	IEnumerator OpeningSequence() {
		cam.FadeToColor(Color.black, 40f);
		MusicControl.PlayOpening();

		yield return new WaitForSeconds(3f);

		cam.FadeToColor(Color.black, 2f);
		cam.SetText("you carry a message...\n\n\n\n...and a warning");
		cam.FadeTextIn();

		yield return new WaitForSeconds(4);

		cam.FadeTextOut();

		yield return new WaitForSeconds(2.5f);

		cam.SetText("you are the FORERUNNER\n\n\n");
		cam.FadeTextIn();
		
		yield return new WaitForSeconds(3);

		cam.FadeToColor(Color.clear, 2f);

		yield return new WaitForSeconds(2);

		cam.FadeTextOut();

		yield return new WaitForSeconds(2);

		cam.FadeToColor(Color.clear, 40f);
		cam.SetText("press [SPACE] to start\n\n\n");
		cam.FadeTextIn();

		while (true) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				break;
			}
			yield return null;
		}

		MusicControl.PlayDay();

		cam.SetText("move with WASD or\n the arrow keys\n\n\n\n");
		StartCoroutine("StartGame");
		
		yield return new WaitForSeconds(3);

		cam.FadeTextOut();

	}

	IEnumerator StartGame() {
		p_input.enabled = true;

		for (int i = 0; i < levels.Length; i++) {
			while (true) { 
				p_input.enabled = true;
				for (int j = 0; j < levels[i].countHazards; j++) {
					yield return new WaitForSeconds(levels[i].delayBetween);
					for (int k = -1; k < levels[i].groupHazards; k++) {
						SpawnRandomHazard(ref levels[i].availHazards);
						yield return new WaitForSeconds(.3f);
					}
				}
				
				if (i == levels.Length - 1) {
					Win ();
				}

				yield return new WaitForSeconds(4.5f);
				//Environment nighttime
				EnvironmentControl.Nighttime();

				if (i == 0)
					cam.SetText("hide from nocturnal pursuers\npress [SPACE] to dive\n\n\n\n");
				if (i == 1)
					cam.SetText("press [SPACE] to surface\n\n\n");
				if (i < 2) {
					cam.FadeToColor(Color.clear,10f);
					cam.FadeTextIn();
				}
				p_dive.enabled = true;
				while (true) {
					if (Input.GetKeyDown(KeyCode.Space)) {
						break;
					}
					yield return null;
				}
				cam.FadeTextOut();
				p_dive.enabled = false;

				yield return new WaitForSeconds(3f);

				break;
			}
		}
	}

	void Win() {
		StopCoroutine("StartGame");
		StartCoroutine("WinSequence");
	}

	IEnumerator WinSequence() {
		p_dive.enabled = false;
		p_input.enabled = false;
		p_movement.MovePlayer(1,1,1f);

		yield return new WaitForSeconds(4f);

		p_movement.MovePlayer(new Vector3(0,0,60),5f);
		cam.FadeToColor(Color.white,2f);

		yield return new WaitForSeconds(2f);

		cam.FadeToColor(Color.black,2f);

		yield return new WaitForSeconds(2f);

		cam.SetText("the warning reached them in time\nyour home is safe once again");
		cam.FadeTextIn();
		yield return new WaitForSeconds(5f);
		cam.FadeTextOut();

		yield return new WaitForSeconds(2f);

		cam.SetText("press [SPACE] to play again");
		cam.FadeToColor(Color.black,40f);
		cam.FadeTextIn();
		
		while (true) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				break;
			}
			yield return null;
		}
		
		cam.FadeTextOut();

		yield return new WaitForSeconds(1f);

		Application.LoadLevel(Application.loadedLevel);
	}

	void DeadEnd() {
		StopCoroutine("StartGame");
		StartCoroutine("DoDeath");
	}

	IEnumerator DoDeath() {
		p_dive.enabled = false;
		p_input.enabled = false;
		p_movement.MovePlayer(new Vector3(0,-10,0),1f);
		
		yield return new WaitForSeconds(.3f);
		
		CameraShake();

		yield return new WaitForSeconds(.3f);

		CameraShake();
		
		yield return new WaitForSeconds(.3f); 
		
		CameraShake();

		yield return new WaitForSeconds(1f);

		cam.FadeToColor(Color.black,2f);

		yield return new WaitForSeconds(2f);

		cam.SetText("but the warning never came...");
		cam.FadeTextIn();

		yield return new WaitForSeconds(3f);

		cam.FadeToColor(Color.black,2f);
		cam.FadeTextOut();

		yield return new WaitForSeconds(2f);
		
		cam.FadeToColor(Color.black,40f);
		cam.SetText("press [SPACE] to continue");
		cam.FadeTextIn();

		while (true) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				break;
			}
			yield return null;
		}

		cam.FadeTextOut();

		EnvironmentControl.Reset();

		yield return new WaitForSeconds(2f);

		StartCoroutine("StartGame");
	}

	void SpawnRandomHazard(ref Hazard[] hazards) {
		Instantiate(hazards[Random.Range(0,hazards.Length)].gameObject);
	}

	void CameraShake() {
		cam.FadeToColor(Color.red,8f);
		StartCoroutine("Shake",3);
	}

	IEnumerator Shake(int shakeCount) {
		while (shakeCount > 0) {
			cam.transform.position += new Vector3(0,.2f);
			yield return new WaitForSeconds(.05f);
			cam.transform.position += new Vector3(0,-.2f);
			yield return new WaitForSeconds(.05f);
			cam.FadeToColor(Color.clear,1f);
			shakeCount --;
		}
		cam.transform.position = Vector3.zero;
	}
}
