using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public PlayerMain playerMain;

	public GameObject hinge;

	public bool opened;
	public bool locked;
	public int lockDC;

	public bool showMessage;
	public string lockedMessage;

	public AudioSource openSound;
	public AudioSource closeSound;
	public AudioSource lockedSound;	
	public AudioSource unlockSound;

	public AudioSource pickingSound;


	void Awake () {
		lockedMessage = "Locked";


	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {

	}

	public void open () {
		openSound.Play ();
		transform.RotateAround(hinge.transform.position, Vector3.up, 90);
		opened = true;
	}

	public void close () {
		closeSound.Play ();
		transform.RotateAround(hinge.transform.position, Vector3.up, -90);
		opened = false;
	}


}
