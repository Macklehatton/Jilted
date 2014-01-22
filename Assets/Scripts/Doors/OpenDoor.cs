using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	public PlayerMain playerMain;
	public ForwardInteraction interact;
	public float openRange;
	public float openAngle;
	public float cooldownTime;
	public float useTimer;

	public bool showMessage;
	public string lockedMessage;
	public Door lockedDoor;
	
	public float lockpickTimer;
	public float timeSpent;
	
	public bool playPickingSound = false;

	void Awake () {
		openRange = 4.0f;
		openAngle = 30.0f;
		cooldownTime = 0.5f;
		useTimer = cooldownTime;
		timeSpent = 0.0f;
	}

	void Start () {
		lockpickTimer = playerMain.baseLockTime - (playerMain.coordination / 2);
		interact = GetComponent<ForwardInteraction>();
	}

	void Update () {
		useTimer += Time.deltaTime;
		if (Input.GetButtonDown ("Activate")) {
			open ();
		}
		if (lockedDoor != null) {
			distanceCheck ();
			if (Input.GetButton ("Activate")) {
				pickLock (lockedDoor);
			} else {
				stopPicking ();
			}		
		}
	}

	void OnGUI () {
		if (showMessage == true){
			lockedMessage = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 55, 25), lockedMessage, 200);
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to pick", 200);
		}
	}

	void open () {
		Door door = interact.getTarget (useTimer, cooldownTime, "Door", openRange, openAngle) as Door;
		if (door != null) { 
			if (door.opened == false) {
				if (door.locked == false) {
					door.open ();
				} else if (door.locked == true) {
					lockedMessage = door.lockedMessage;
					showMessage = true;
					lockedDoor = door;
				}
			} else if (door.opened == true) {
				door.close ();
			}
		}
	}

	void pickLock (Door door) {
		if (door != null) { //This double check gets rid of a null reference
			timeSpent += Time.deltaTime;
			if (door.locked = true) {
				if (playPickingSound == false) {
					playPickingSound = true;
					door.pickingSound.Play ();
				} 
				if (timeSpent >= lockpickTimer) {
					door.unlockSound.Play ();
					door.locked = false;
					showMessage = false;
					stopPicking();
				}
			}
		}
	}

	void distanceCheck () {
		if (Vector3.Distance (lockedDoor.transform.position, transform.position) > openRange) {
			stopPicking ();
			showMessage = false;
		}
	}

	void stopPicking () {
		lockedDoor.pickingSound.Stop ();
		playPickingSound = false;
		timeSpent = 0.0f;
		lockedDoor = null;
	}
}
