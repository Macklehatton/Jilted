//using UnityEngine;
//using System.Collections;
//using System.Linq;
//
//public class ActivateGeneratorOld : MonoBehaviour {
//
//	public PlayerMain playerMain;
//
//	Vector3 forward;
//	Vector3 playerPosition;
//	
//	public float openRange;
//	public float openAngle;
//	public float cooldownTime;
//	public float useTimer;
//	public bool showBrokenMessage;
//	public string brokenMessage;
//	public bool showFuelMessage;
//	public string needFuelMessage;
//
//
//	public Generator brokenGenerator;
//	
//	public float repairTimer;
//	public bool playRepairSound;
//
//	void Awake () {
//		openRange = 4.0F;
//		openAngle = 30.0F;
//		cooldownTime = 0.5F;
//		useTimer = cooldownTime;
//		showBrokenMessage = false;
//
//		playRepairSound = false;
//
//	}
//
//	// Use this for initialization
//	void Start () {
//		repairTimer = playerMain.baseRepairTime - (playerMain.coordination / 2);		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		Vector3 forward = transform.forward;
//		Vector3 playerPosition = transform.position;
//		
//		useTimer += Time.deltaTime;
//		
//		if (Input.GetButtonDown("Activate")){
//			if (showBrokenMessage == false) {
//				useGenerator(playerPosition, forward);
//			}
//		}
//
//		if (showBrokenMessage == true){
//			if (useTimer >= cooldownTime){
//				if (Input.GetButton("Activate")){
//					repair(brokenGenerator);
//					if (playRepairSound == true && brokenGenerator.repairSound.isPlaying == false){
//						brokenGenerator.repairSound.Play ();
//					}else if (playRepairSound == false && brokenGenerator.repairSound.isPlaying == true){
//						brokenGenerator.repairSound.Stop ();
//					}
//				}else if (playRepairSound == true) {
//					brokenGenerator.repairSound.Stop ();
//					repairTimer =  playerMain.baseRepairTime - (playerMain.coordination / 2);
//				}
//			}
//		}
//	}
//
//	void OnGUI () {
//		if (showBrokenMessage == true){
//			brokenMessage = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 55, 25), brokenMessage, 200);
//			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to repair", 200);
//		}
//	}
//
//
//	void useGenerator(Vector3 playerPosition, Vector3 forward){
//		if (useTimer >= cooldownTime){
//			useTimer = 0.0F;
//			
//			Collider[] colliders = spherecastColliders(playerPosition);
//			
//			foreach (Collider entity in colliders){
//				Generator generator = entity.GetComponent<Generator>();
//				
//				if (generator) {
//					activateGenerator (generator, playerPosition, forward);
//				}
//			}			
//		}
//	}
//
//	void activateGenerator(Generator generator, Vector3 playerPosition, Vector3 forward){
//		Debug.Log( "Activate generator called");
//		
//		Vector3 targetDir = generator.transform.position - playerPosition;
//		
//		if(calcAngle(playerPosition, generator, forward, targetDir) <= openAngle){
//			if (raycastLOS(playerPosition, targetDir)){
//				if (generator.broken == false){
//					if (generator.running == false){
//						if (generator.hasFuel == true){
//							generator.run ();						
//						}
//					} else {
//						generator.stopSound.Play();
//						generator.stop ();
//						
//					}				
//				}else if (generator.broken == true) {
//					brokenMessage = generator.brokenMessage;
//					showBrokenMessage = true;
//					brokenGenerator = generator;
//				}else if (generator.hasFuel == false) {
//					needFuelMessage = generator.needFuelMessage;
//					showFuelMessage = true;
//				}
//			}
//		}
//	}
//	
//	
//	
//	Collider[] spherecastColliders(Vector3 playerPosition){
//		Collider[] colliders = Physics.OverlapSphere(playerPosition, openRange);
//		colliders = colliders.OrderBy(x => Vector3.Distance(x.transform.position, playerPosition)).ToArray();
//		return colliders;
//	}
//	
//	float calcAngle(Vector3 playerPosition, Generator generator, Vector3 forward, Vector3 targetDir) {
//		float angle = Vector3.Angle(forward, targetDir);
//		return angle;
//	}
//	
//	bool raycastLOS(Vector3 playerPosition, Vector3 targetDir){
//		RaycastHit hit;
//		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		Physics.Raycast(playerPosition, targetDir, out hit);
//		
//		if (hit.collider){
//			return true;
//		}
//		else {
//			return false;
//		}
//	}
//
//	void repair(Generator generator){
//		Debug.Log ("Repairing");
//		
//		
//		if (playerMain.mechanical >= 1){
//			repairTimer -= Time.deltaTime * 1;
//			Debug.Log (repairTimer);
//			
//			playRepairSound = true;
//			
//			if (repairTimer <= 0.0F){
//				
//				playRepairSound = false;
//
//				int repairRoll = Random.Range(1,100);
//				Debug.Log (repairRoll + playerMain.mechanical);		
//				
//
//				if (playerMain.wisdom + repairRoll > generator.repairDC){
//					generator.fixedSound.Play ();
//					generator.broken = false;
//					showBrokenMessage = false;
//				}else {
//					Debug.Log("Repair failed");
//					showBrokenMessage = false;
//				}			
//			}
//		}else {
//			Debug.Log ("Generator is broken, you don't have the skill to fix it.");
//		}
//	}
//}
//
