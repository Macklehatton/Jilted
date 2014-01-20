using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public bool hasFuel = true;
	public bool broken = false;
	public bool running = false;

	public AudioSource startupSound;
	public AudioSource runningSound;
	public AudioSource stopSound;
	public AudioSource brokenSound;
	public AudioSource repairSound;
	public AudioSource fixedSound;

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
	
	}

	public void run () {
		Debug.Log ("Run generator");
		running = true;
		startupSound.Play ();
		doorSwitch.powered = true;
	}

	public void stop () {
		Debug.Log ("Stop generator");
		stopSound.Play();
		running = false;
	}
}
