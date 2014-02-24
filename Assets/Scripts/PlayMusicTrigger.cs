using UnityEngine;
using System.Collections;

public class PlayMusicTrigger : MonoBehaviour {
	public AudioSource triggeredSound;
	public bool once;

	bool played;

	void OnTriggerEnter () {
		if (once == true && played == false) {			
			triggeredSound.Play();
			played = true;
		}else {
			triggeredSound.Play();
		}
	}
}
