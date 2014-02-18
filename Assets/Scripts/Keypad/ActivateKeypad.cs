using UnityEngine;
using System.Collections;

public class ActivateKeypad : MonoBehaviour {

	public ForwardInteraction interact;

	public float activationRange;
	public float activationAngle;

	void Awake () {
		interact = GetComponent<ForwardInteraction>();

		activationRange = 2.0f;
		activationAngle = 30.0f;
	}

	void Start () {
	
	}
	
	void Update () {
		if (Input.GetButtonDown("Activate")) {
			Keypad keypad = interact.getTarget(1, 0, "Keypad", activationRange, activationAngle) as Keypad;
			if (keypad != null) {
				keypad.showKeypad = true;
			}
		}
	}
}
