using UnityEngine;
using System;
using System.Collections;

public class PlayerDamageControl : MonoBehaviour {

	public int playerHealth = 3;
	public float postDamageInvuln = 1f;
	public bool invulnerable = false;

	public event Action playerDamaged;
	public event Action playerDied;

	private GameObject body;

	void Awake() {
		body = transform.FindChild("Body").gameObject;
	}

	void Start() {
		playerDamaged += Damaged;
		playerDied += Dead;
	}

	void OnTriggerEnter(Collider col) {
		if (!invulnerable && col.gameObject.tag == "Hazard") {
			playerDamaged();
		}
	}

	void Damaged() {
		MusicControl.PlayDamage();
		playerHealth--;
		if (playerHealth <= 0)
			playerDied();
		else 
			StartCoroutine("Invulnerable",postDamageInvuln);
	}

	IEnumerator Invulnerable(float duration) {
		invulnerable = true;
		float remaining = duration;
		while (remaining > 0) {
			body.SetActive(!body.activeSelf);
			remaining -= .15f;
			yield return new WaitForSeconds(.15f);
		}
		body.SetActive(true);
		invulnerable = false;
	}

	void Dead() {
		StartCoroutine("Invulnerable",10f);
		playerHealth = 3;
	}
}
