using UnityEngine;
using System.Collections;

public struct PlayerLocation {

	public Vector3 pos;
	public Quaternion rot;

	public PlayerLocation(Vector3 pos, Quaternion rot) {
		this.pos = pos;
		this.rot = rot;
	}
}

public class PlayerMovementControl : MonoBehaviour {

	public float movementFromCenter = 4f;

	public float movementSpeed = 4f;

	//public float horizontalRotation = 75f;
	//public float verticalRotation = 0f;

	private float origMovementSpeed;

	private Transform t;
	private PlayerLocation currentLoc;
	private PlayerLocation[,] locationArray = new PlayerLocation[3,3];

	void Awake() {
		t= transform;

		//Set location array
		locationArray[0,0] = new PlayerLocation(new Vector3(-1,1)	* movementFromCenter + (Vector3)Vector2.up, Quaternion.Euler(0,0,-55f));
		locationArray[0,1] = new PlayerLocation(new Vector3(-1,0)	* movementFromCenter + (Vector3)Vector2.up, Quaternion.Euler(0,0,-75f));
		locationArray[0,2] = new PlayerLocation(new Vector3(-1,-1)	* movementFromCenter			 , Quaternion.Euler(0,0,-55f));
		
		locationArray[1,0] = new PlayerLocation(new Vector3(0,1)	* movementFromCenter			 , Quaternion.identity);
		locationArray[1,1] = new PlayerLocation(Vector3.zero										 , Quaternion.identity);
		locationArray[1,2] = new PlayerLocation(new Vector3(0,-1)	* movementFromCenter			 , Quaternion.identity);

		locationArray[2,0] = new PlayerLocation(new Vector3(1,1)	* movementFromCenter + (Vector3)Vector2.up, Quaternion.Euler(0,0, 55f));
		locationArray[2,1] = new PlayerLocation(new Vector3(1,0)	* movementFromCenter + (Vector3)Vector2.up, Quaternion.Euler(0,0, 75f));
		locationArray[2,2] = new PlayerLocation(new Vector3(1,-1)	* movementFromCenter			 , Quaternion.Euler(0,0, 55f));

		currentLoc = locationArray[1,1];

		origMovementSpeed = movementSpeed;
	}
	
	public void MovePlayer(int x, int y) {
		currentLoc = locationArray[x,y];
		movementSpeed = origMovementSpeed;
	}

	public void MovePlayer(int x, int y, float speed) {
		currentLoc = locationArray[x,y];
		movementSpeed = speed;
	}

	public void MovePlayer(Vector3 target, float speed) {
		currentLoc = new PlayerLocation(target, Quaternion.identity);
		movementSpeed = speed;
	}

	void Update() {

		t.position = Vector3.Lerp(t.position, currentLoc.pos, Time.deltaTime * movementSpeed);
		t.rotation = Quaternion.Lerp(t.rotation, currentLoc.rot, Time.deltaTime * 10);
	}
}
