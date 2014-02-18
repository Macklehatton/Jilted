using UnityEngine;
using System.Collections;

public class Crowbar : MonoBehaviour {

	public ItemSelection selected;
	public ForwardInteraction interact;
	public float pryRange;
	public float pryAngle;

	void Awake () {
		selected = GameObject.Find ("Player").GetComponent<ItemSelection>();
		interact = GameObject.Find ("Graphics").GetComponent<ForwardInteraction>();
	}
	
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			if (selected.selectedWeapon == 1) {
				pryDoor ();
			}
		}
	}

	void pryDoor () {
		JammedDoor jammedDoor = interact.getTarget(1, 0, "JammedDoor", pryRange, pryAngle) as JammedDoor;
		if (jammedDoor != null) {
			jammedDoor.open = true;
		}
	}
}
