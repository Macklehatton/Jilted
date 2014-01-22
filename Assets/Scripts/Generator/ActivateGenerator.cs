using UnityEngine;
using System.Collections;

public class ActivateGenerator : MonoBehaviour {
	
	public PlayerMain playerMain;
	public ForwardInteraction interact;
	public float activationRange;
	public float activationAngle;
	public float cooldownTime;
	public float useTimer;

	public bool canRun;

	public bool showMessage;
	public string brokenMessage;
	public Generator brokenGenerator;
	
	public float repairTimer;
	public float timeSpent;
	
	public bool playRepairSound = false;
	
	void Awake () {
		activationRange = 4.0f;
		activationAngle = 30.0f;
		cooldownTime = 0.5f;
		useTimer = cooldownTime;
		timeSpent = 0.0f;
	}
	
	void Start () {
		repairTimer = playerMain.baseRepairTime - (playerMain.coordination / 2);
		interact = GetComponent<ForwardInteraction>();
	}
	
	void Update () {
		useTimer += Time.deltaTime;
		if (Input.GetButtonDown ("Activate")) {
			activate ();
		}
		if (brokenGenerator != null) {
			Debug.Log (brokenGenerator);
			distanceCheck ();
			if (Input.GetButton ("Activate")) {
				repair (brokenGenerator);
			} else {
				stopRepairing ();
			}		
		}
	}
	
	void OnGUI () {
		if (showMessage == true){
			brokenMessage = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 55, 25), brokenMessage, 200);
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to repair", 200);
		}
	}
	
	void activate () {
		Generator generator = interact.getTarget (useTimer, cooldownTime, "Generator", activationRange, activationAngle) as Generator;

		if (generator != null) {
			canRunCheck (generator);
			if (canRun == true) {
				generator.run ();			
			} else if (generator.broken == true) {
				brokenMessage = generator.brokenMessage;
				showMessage = true;
				brokenGenerator = generator;			
			} else if (generator.hasFuel == false) {

			} else if (generator.running == true) {
				generator.stop ();
			}
		}
	}

	void canRunCheck (Generator generator) {
		if (generator.broken == true) {
			canRun = false;
		} if (generator.hasFuel == false){
			canRun = false;
		} if (generator.running = true) {
			canRun = false;		
		} else {
			canRun = true;
		}
	}

	
	void repair (Generator generator) {
		if (generator != null) { //This double check gets rid of a null reference
			timeSpent += Time.deltaTime;
			if (generator.broken = true) {
				if (playRepairSound == false) {
					playRepairSound = true;
					generator.repairSound.Play ();
				} 
				if (timeSpent >= repairTimer) {
					generator.fixedSound.Play ();
					generator.broken = false;
					showMessage = false;
					stopRepairing();
				}
			}
		}
	}
	
	void distanceCheck () {
		if (Vector3.Distance (brokenGenerator.transform.position, transform.position) > activationRange) {
			stopRepairing ();
			showMessage = false;
		}
	}
	
	void stopRepairing () {
		brokenGenerator.repairSound.Stop ();
		playRepairSound = false;
		timeSpent = 0.0f;
		brokenGenerator = null;
	}

	
}
