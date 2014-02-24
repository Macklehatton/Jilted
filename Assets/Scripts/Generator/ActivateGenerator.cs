using UnityEngine;
using System.Collections;

/// <summary>
/// Checks for a generator in front of the player on key press, handles repairing and refueling, activation.
/// </summary>

public class ActivateGenerator : MonoBehaviour {
	
	public PlayerMain playerMain;
	public InventoryMain inventory;
	public ForwardInteraction interact;
	public float activationRange;
	public float activationAngle;
	public float cooldownTime;
	public float useTimer;

	public bool canRun;

	public bool showBroken;
	public string brokenMessage;
	public Generator brokenGenerator;	
	public float repairTimer;
	public float timeRepairing;	
	public bool playRepairSound = false;

	public bool showNeedFuel;
	public string needFuelMessage;
	public Generator dryGenerator;
	public float fuelTimer;
	public float timeFueling;
	public bool playFuelingSound = false;

	public Generator lastActivated;


	void Awake () {
		canRun = true;
		activationRange = 2.5f;
		activationAngle = 30.0f;
		cooldownTime = 0.5f;
		useTimer = cooldownTime;
		timeRepairing = 0.0f;
		fuelTimer = 3.0f;
		timeFueling = 0.0f;
	}
	
	void Start () {
		repairTimer = playerMain.baseRepairTime - (playerMain.coordination / 2);
		interact = GetComponent<ForwardInteraction>();
		inventory = transform.FindChild("Camera").GetComponent<InventoryMain>();
	}
	
	void Update () {

		useTimer += Time.deltaTime;
		if (Input.GetButtonDown ("Activate")) {
			activate ();
		}
		if (brokenGenerator != null) {
			if (Input.GetButton ("Activate")) {
				repair (brokenGenerator);
			} else {
				stopRepairing ();
			}		
		}
		if (dryGenerator != null) {
			if (Input.GetButton ("Activate")) {
				refuel (dryGenerator);
			} else {
				stopFueling ();
			}		
		}

		if (lastActivated != null) {
			distanceCheck (dryGenerator);
		}
	}
	
	void OnGUI () {
		if (showBroken == true){
			GUI.TextArea(new Rect(Screen.width / 2 - 55, Screen.height / 2 - 25, 55, 25), brokenMessage, 200);
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to repair", 200);
		}
		if (showNeedFuel == true){
			GUI.TextArea(new Rect(Screen.width / 2 - 55, Screen.height / 2 + 25, 55, 25), brokenMessage, 200);
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to fuel", 200);
		}

	}
	
	void activate () {
		Generator generator = interact.getTarget (useTimer, cooldownTime, "Generator", activationRange, activationAngle) as Generator;
		lastActivated = generator;
		if (generator != null) {
			canRunCheck (generator);
			if (canRun == true) {
				generator.run ();			
			} else if (generator.broken == true) {
				brokenMessage = generator.brokenMessage;
				showBroken = true;
				brokenGenerator = generator;			
			} else if (generator.needFuel == true) {
				needFuelMessage = generator.needFuelMessage;
				showNeedFuel = true;
				dryGenerator = generator;
			} else if (generator.running == true) {
				generator.stop ();
			}
		}
	}

	void canRunCheck (Generator generator) {
		if (generator.broken == true) {
			canRun = false;
			return;
		} if (generator.needFuel == true){
			canRun = false;
			return;
		} if (generator.running == true) {
			canRun = false;	
			return;
		} else {
			canRun = true;
		}
	}

	
	void repair (Generator generator) {
		if (generator != null) { //This double check gets rid of a null reference
			timeRepairing += Time.deltaTime;
			if (generator.broken = true) {
				if (playRepairSound == false) {
					playRepairSound = true;
					generator.repairSound.Play ();
				} 
				if (timeRepairing >= repairTimer) {
					generator.fixedSound.Play ();
					generator.broken = false;
					showBroken = false;
					stopRepairing();
				}
			}
		}
	}

	void refuel (Generator generator) {
		if (generator != null) { //This double check gets rid of a null reference
			if (inventory.items[0].carried > 0) {
				timeFueling += Time.deltaTime;
				if (generator.needFuel = true) {
					if (playFuelingSound == false) {
						playFuelingSound = true;
						generator.fuelingSound.Play ();
					} 
					if (timeFueling >= fuelTimer) {
						generator.filledSound.Play ();
						generator.needFuel = false;
						showNeedFuel = false;
						inventory.items[0].carried -= 1;
						stopFueling();
					}
				}
			} else {
				Debug.Log ("Need fuel can");
			}
		}
	}
	
	void distanceCheck (Generator generator) {
		Debug.Log(Vector3.Distance (lastActivated.transform.position, transform.position));
		if (Vector3.Distance (lastActivated.transform.position, transform.position) > activationRange) {			
			showBroken = false;
			showNeedFuel = false;
			stopRepairing ();
			stopFueling ();
			lastActivated = null;
		}
	}
	
	void stopRepairing () {
		if (brokenGenerator != null) {
			brokenGenerator.repairSound.Stop ();
			playRepairSound = false;
			timeRepairing = 0.0f;
			brokenGenerator = null;
		}
	}

	void stopFueling () {
		if (dryGenerator != null) {
			dryGenerator.fuelingSound.Stop ();
			playFuelingSound = false;
			timeFueling = 0.0f;
			dryGenerator = null;				
		}
	}

	
}
