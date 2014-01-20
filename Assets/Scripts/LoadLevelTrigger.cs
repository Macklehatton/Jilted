using UnityEngine;
using System.Collections;

public class LoadLevelTrigger : MonoBehaviour {

	public float triggerRadius;
	public string levelName;
	public GameObject player;

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) < triggerRadius){
			Application.LoadLevel(levelName);
		}
	}
}
