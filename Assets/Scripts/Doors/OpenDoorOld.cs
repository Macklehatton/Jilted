//using UnityEngine;
//using System.Collections;
//using System.Linq;
//
//public class OpenDoorOld : MonoBehaviour {
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
//	public bool showMessage;
//	public string lockedMessage;
//	public Door lockedDoor;
//
//	public float lockpickTimer;
//
//	public bool playPickingSound;
//
//
//
//	void Awake () {
//		openRange = 4.0F;
//		openAngle = 30.0F;
//		cooldownTime = 0.5F;
//		useTimer = cooldownTime;
//		showMessage = false;
//		playPickingSound = false;
//
//
//	}
//
//	// Use this for initialization
//	void Start () {
//		lockpickTimer =  playerMain.baseLockTime - (playerMain.coordination / 2);	
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
//			if (showMessage == false) {
//				useDoor(playerPosition, forward);
//			}
//		}
//
//		if (showMessage == true){
//			if (useTimer >= cooldownTime){
//				if (Input.GetButton("Activate")){
//					pickLock(lockedDoor);
//					if (playPickingSound == true && lockedDoor.pickingSound.isPlaying == false){
//						lockedDoor.pickingSound.Play ();
//					}else if (playPickingSound == false && lockedDoor.pickingSound.isPlaying == true){
//						lockedDoor.pickingSound.Stop ();
//					}
//				}else if (playPickingSound == true) {
//					lockedDoor.pickingSound.Stop ();
//					lockpickTimer =  playerMain.baseLockTime - (playerMain.coordination / 2);
//				}
//			}
//		}
//	}
//
//	void OnGUI () {
//		if (showMessage == true){
//			lockedMessage = GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2, 55, 25), lockedMessage, 200);
//			GUI.TextArea(new Rect(Screen.width / 2, Screen.height / 2 + 35, 60, 35), "Hold E to pick", 200);
//		}
//	}
//
////	//Always returns null?
////	Door findDoor(){
////		Collider[] colliders = spherecastColliders(playerPosition);
////
////		foreach (Collider entity in colliders){
////			Door door = entity.GetComponent<Door>();
////
////			if (door) {
////				return door;
////			}
////		}
////
////		return null;
////	}
//
//	void useDoor(Vector3 playerPosition, Vector3 forward){
//		if (useTimer >= cooldownTime){
//			useTimer = 0.0F;
//
//			Collider[] colliders = spherecastColliders(playerPosition);
//
//			foreach (Collider entity in colliders){
//				Door door = entity.GetComponent<Door>();
//				
//				if (door) {
//					openDoor (door, playerPosition, forward);
//				}
//			}			
//		}
//	}
//
//
//	void openDoor(Door door, Vector3 playerPosition, Vector3 forward){
//		Debug.Log( "Opendoor called");
//
//		Vector3 targetDir = door.transform.position - playerPosition;
//		
//		if(calcAngle(playerPosition, door, forward, targetDir) <= openAngle){
//			if (raycastLOS(playerPosition, targetDir)){
//				if (door.isLocked == false){
//					if (door.opened == false){
//						door.openSound.Play();
//						door.open ();
//
//					} else {
//						door.closeSound.Play();
//						door.close ();
//
//					}				
//				}else {
//					lockedMessage = door.lockedMessage;
//					showMessage = true;
//					lockedDoor = door;
//					door.lockedSound.Play();								
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
//	float calcAngle(Vector3 playerPosition, Door door, Vector3 forward, Vector3 targetDir) {
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
//	void pickLock(Door door){
//		Debug.Log ("Picking lock");
//		
//
//		if (playerMain.lockpick >= 1){
//			lockpickTimer -= Time.deltaTime * 1;
//			Debug.Log (lockpickTimer);
//
//			playPickingSound = true;
//
//			if (lockpickTimer <= 0.0F){
//
//				playPickingSound = false;					
//
//				int lockpickRoll = Random.Range(1,100);
//				Debug.Log (lockpickRoll + playerMain.lockpick);	
//
//				if (playerMain.coordination + lockpickRoll > door.lockDC){
//					door.unlockSound.Play();
//					door.isLocked = false;
//					showMessage = false;
//				}else {
//					Debug.Log("Lockpicking failed");
//					showMessage = false;
//				}			
//			}
//		}else {
//			Debug.Log ("Door is locked, you don't have the skill to unlock it.");
//		}		
//	}
//
//
//}
