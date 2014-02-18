using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour {

	public DoorSwitch doorSwitch;
	public GameObject redButton;
	public GameObject greenButton;
	public bool locked = true;
	public bool showKeypad = false;
	public string stringToEdit = "";
	public string passkey;
	


	void Awake () {
		passkey = "1234";

	}
	
	void Update () {
		if (locked == false) {
			greenButton.renderer.material.color = Color.green;
			redButton.renderer.material.color = Color.grey;
		} else {
			greenButton.renderer.material.color = Color.gray;
			redButton.renderer.material.color = Color.red;
		}
	}


	void OnGUI() {
		if (showKeypad == true) {
			stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
			if (Event.current.keyCode == KeyCode.Return) {
				if (stringToEdit == passkey) {
					locked = false;
					showKeypad = false;
					doorSwitch.enabled = true;
				}
			}
		}
	}
}
