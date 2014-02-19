using UnityEngine;
using System.Collections;

public class JammedDoor : MonoBehaviour {

	public bool open = false;
	public float moveTimer = 0.5f;

	public AudioSource openSound;

	// Update is called once per frame
	void Update () {
		if (open == true) {
			moveTimer -= Time.deltaTime;
			if (moveTimer > 0) {
				transform.Translate(Vector3.back * Time.deltaTime);
				if (openSound.isPlaying == false) {
					openSound.Play ();
				}
			}


			
		}
	
	}
}
