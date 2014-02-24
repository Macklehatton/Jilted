using UnityEngine;
using System.Collections;

public class FadeTrigger : MonoBehaviour {

	public CameraFade fader;
	public CharacterControls controller;
	public bool entered;
	public float timer;
	public float timePassed;

	void Awake () {
		fader = (CameraFade)Camera.main.GetComponent("CameraFade");

	}

	void OnTriggerEnter () {
		entered = true;
		Debug.Log ("Entered");
	}

	void Update() {
		if (entered == true) {
			timePassed += Time.deltaTime;
		}

		if (timePassed > timer) {
			fader.FadeOut();
			controller.enabled = false;
		}
	}
}
