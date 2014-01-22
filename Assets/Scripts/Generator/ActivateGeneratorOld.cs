using UnityEngine;
using System.Collections;

public class ActivateGeneratorOld : MonoBehaviour {

	public PlayerMain playerMain;
	public ForwardInteraction interact;

	public float activationRange;
	public float activationAngle;
	public float cooldownTime;
	public float useTimer;
	public bool showBrokenMessage;
	public string brokenMessage;
	public bool showFuelMessage;
	public string needFuelMessage;
	public Generator brokenGenerator;	
	public float repairTimer;
	public bool repairSoundPlaying;
	
	void Awake () {
		interact = GetComponent<ForwardInteraction>();
		activationRange = 5.0f;
		activationAngle = 30.0f;
		cooldownTime = 0.5f;
		useTimer = cooldownTime;
		showBrokenMessage = false;		
		repairSoundPlaying = false;		
	}
	
	void Start () {
		repairTimer = playerMain.baseRepairTime - (playerMain.coordination / 2);		
	}
	
	void Update () {		
		useTimer += Time.deltaTime;		
		if (Input.GetButtonDown("Activate")){
			if (showBrokenMessage == false) {
				activate();
			} else if (showBrokenMessage == true) {
				repair(brokenGenerator);
			}
		}
	}
	
	void OnGUI () {
		if (showBrokenMessage == true){
			brokenMessage = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 55, 25), brokenMessage, 200);
			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to repair", 200);
		}
	}
			
	void activate(){
		Generator generator = interact.getTarget(useTimer, cooldownTime, "Generator", activationRange, activationAngle) as Generator;
		Debug.Log (generator);
		if (generator != null) {
			if (canRun (generator) == true){
				generator.run ();													
			} else if (generator.broken == true) {
				brokenMessage = generator.brokenMessage;
				showBrokenMessage = true;
				brokenGenerator = generator;
			} else if (generator.hasFuel == false) {
				needFuelMessage = generator.needFuelMessage;
				showFuelMessage = true;
			} else {
				generator.stop ();
			}
		}
	}

	bool canRun (Generator generator) {
		if (generator.broken == false){
			if (generator.running == false){
				if (generator.hasFuel == true){
					return true;
				}
			}
		}
		return false;
	}

	void repair(Generator generator){
		if (Input.GetButton ("Activate")){
			if (playerMain.mechanical >= 1){
				repairTimer -= Time.deltaTime * 1;
				Debug.Log (repairTimer);			
				repairSoundPlaying = true;			
				if (repairTimer <= 0.0F){				
					repairSoundPlaying = false;				
					int repairRoll = Random.Range(1,100);
					Debug.Log (repairRoll + playerMain.mechanical);				
					if (playerMain.wisdom + repairRoll > generator.repairDC){
						generator.fixedSound.Play ();
						generator.broken = false;
						showBrokenMessage = false;
					}else {
						showBrokenMessage = false;
					}			
				}
			}else {
				Debug.Log ("Generator is broken, you don't have the skill to fix it");
			}
		}
	}

	void handleRepairSound () {
		if (repairSoundPlaying == true && brokenGenerator.repairSound.isPlaying == false){
			brokenGenerator.repairSound.Play ();
		}else if (repairSoundPlaying == false && brokenGenerator.repairSound.isPlaying == true){
			brokenGenerator.repairSound.Stop ();
		}else if (repairSoundPlaying == true) {
			brokenGenerator.repairSound.Stop ();
			repairTimer =  playerMain.baseRepairTime - (playerMain.coordination / 2);
		}
	}


}
