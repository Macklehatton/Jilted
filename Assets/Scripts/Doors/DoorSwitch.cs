﻿using UnityEngine;
using System.Collections;

public class DoorSwitch : MonoBehaviour {

	public GameObject player;
	public GameObject door;

	public float activateDistance;
	public float moveTimer;

	public AudioSource button;
	public AudioSource bigDoor;

	public bool powered = true;

	bool open;
	bool activate;

	// Use this for initialization
	void Awake () {
		activate = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Activate")) {

			if (Vector3.Distance(transform.position, player.transform.position) < activateDistance){
				Debug.Log ("Activate switch");
				if (button.isPlaying == false) {
					button.Play ();
				}

				if (powered == true){
					activate = true;

					if (open == false) {
						if (bigDoor.isPlaying == false) {
							bigDoor.Play();
						}
					}
				}
			}
		}

		if (activate == true){
			if (open == false){
				if (moveTimer > 0){
					door.transform.Translate(Vector3.forward * 10 * Time.deltaTime);
					moveTimer -= Time.deltaTime * 1;
				}
				if (moveTimer <= 0){
					open = true;
				}

			}
		}
	}
}