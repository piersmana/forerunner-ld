using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {

	private static AudioSource player;

	public AudioClip _openingTheme;
	public AudioClip _dayMusic;
	public AudioClip _nightMusic;
	public AudioClip _deathMusic;
	public AudioClip _winMusic;
	public AudioClip _damageSFX;
	
	private static AudioClip openingTheme;
	private static AudioClip dayMusic;
	private static AudioClip nightMusic;
	private static AudioClip deathMusic;
	private static AudioClip winMusic;
	private static AudioClip damageSFX;

	void Awake() {
		player = GetComponent<AudioSource>();

		openingTheme = _openingTheme;
		dayMusic = _dayMusic;
		nightMusic = _nightMusic;
		deathMusic = _deathMusic;
		winMusic = _winMusic;
		damageSFX = _damageSFX;
	}

	public static void PlayOpening() {
		player.clip = openingTheme;
		player.loop = false;
		player.Play();
	}

	public static void PlayDay() {
		player.clip = dayMusic;
		player.loop = true;
		player.Play();
	}

	public static void PlayNight() {
		player.clip = nightMusic;
		player.loop = true;
		player.Play();
	}

	public static void PlayDamage() {
		player.PlayOneShot(damageSFX);
	}
}
