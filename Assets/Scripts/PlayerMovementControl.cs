using UnityEngine;
using System.Collections;

public struct PlayerPosRot {

	public Vector2 pos;
	public Quaternion rot;

	public PlayerPosRot(Vector2 pos, Quaternion rot) {
		this.pos = pos;
		this.rot = rot;
	}
}

public class PlayerMovementControl : MonoBehaviour {

	public float movementFromCenter = 4f;

	public float movementSpeed = 4f;

	//public float horizontalRotation = 75f;
	//public float verticalRotation = 0f;

	private Transform t;
	private PlayerPosRot[,] locationArray = new PlayerPosRot[3,3];

	void Awake() {
		t= transform;

		locationArray[0,0] = new PlayerPosRot(new Vector2(-1,1)	* movementFromCenter + Vector2.up, Quaternion.Euler(0,0,-55f));
		locationArray[0,1] = new PlayerPosRot(new Vector2(-1,0)				* movementFromCenter + Vector2.up, Quaternion.Euler(0,0,-75f));
		locationArray[0,2] = new PlayerPosRot(new Vector2(-1,-1)	* movementFromCenter			 , Quaternion.Euler(0,0,-55f));
		
		locationArray[1,0] = new PlayerPosRot(new Vector2(0,1)				* movementFromCenter, Quaternion.identity);
		locationArray[1,1] = new PlayerPosRot(Vector2.zero										, Quaternion.identity);
		locationArray[1,2] = new PlayerPosRot(new Vector2(0,-1)				* movementFromCenter, Quaternion.identity);

		locationArray[2,0] = new PlayerPosRot(new Vector2(1,1)	* movementFromCenter + Vector2.up, Quaternion.Euler(0,0, 55f));
		locationArray[2,1] = new PlayerPosRot(new Vector2(1,0)				* movementFromCenter + Vector2.up, Quaternion.Euler(0,0, 75f));
		locationArray[2,2] = new PlayerPosRot(new Vector2(1,-1)	* movementFromCenter			 , Quaternion.Euler(0,0, 55f));
	}

	void Start() {
		StartCoroutine("PlayerMovementInput");
	}

	IEnumerator PlayerMovementInput() {
		PlayerPosRot targetLoc;

		while (true) {
			targetLoc = locationArray[1 + (int)Input.GetAxis("Horizontal"), 1 - (int)Input.GetAxis("Vertical")];

			t.position = Vector3.Lerp(t.position, targetLoc.pos, Time.deltaTime * movementSpeed);
			t.rotation = Quaternion.Lerp(t.rotation, targetLoc.rot, Time.deltaTime * 10);

			yield return null;
		}
	}
}
