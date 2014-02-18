using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

	public AudioSource flaslightButton;

	void Update () {
		if (Input.GetButtonDown("Flashlight")) {
			flaslightButton.Play();
			if (light.intensity > 0.0f) {
				light.intensity = 0.0f;
			} else {
				light.intensity = 1.0f;
			}
		}	
	}
}
