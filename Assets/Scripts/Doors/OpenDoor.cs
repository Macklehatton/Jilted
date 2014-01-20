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
	
	public bool playPickingSound;

	void Awake () {
		interact = GetComponent<ForwardInteraction>();
		openRange = 4.0F;
		openAngle = 30.0F;
		cooldownTime = 0.5F;
		useTimer = cooldownTime;
	
	}
	
	void Update () {
		if (Input.GetButtonDown ("Activate")) {
			open ();
		}
	}

	void open () {
		Door door = interact.getTarget (useTimer, cooldownTime, "Door", openRange, openAngle) as Door;
		Debug.Log (door);
		if (door != null) {
			if (door.opened == false) {
				if (door.locked == false) {
					door.open ();
				} else if (door.locked == true) {
					showMessage = true;
				}
			} else if (door.opened == true) {
				door.close ();
			}
		}
	}
}
