using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public bool needFuel = false;
	public bool broken = false;
	public bool running = false;

	public AudioSource startupSound;
	public AudioSource runningSound;
	public AudioSource stopSound;

	public AudioSource brokenSound;
	public AudioSource repairSound;
	public AudioSource fixedSound;

	public AudioSource drySound;
	public AudioSource fuelingSound;
	public AudioSource filledSound;

	public string brokenMessage;
	public float repairDC;

	public string needFuelMessage;

	public DoorSwitch doorSwitch;


	void Awake () {
		brokenMessage = "Broken";
		needFuelMessage = "Needs fuel";
	}
	
	// Update is called once per frame
	void Update () {
		if (running == true) {
			if (startupSound.isPlaying == false) {
				if (runningSound.isPlaying == false) {
					runningSound.Play();			
				}
			}
		}
	}

	public void run () {
		startupSound.Play ();
		doorSwitch.powered = true;
		running = true;
	}

	public void stop () {
		stopSound.Play();
		running = false;
	}
}
