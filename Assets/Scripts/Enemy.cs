using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int stamina = 100;
	public int deflection = 20;
	public int damageReduction = 20;
	public int ballisticThreshold = 50;
	public int condition = 5;

	public Material red;

	// Use this for initialization
	void Start () {
		red = Resources.Load("red") as Material;	
	}
	
	// Update is called once per frame
	void Update () {
		if (stamina <= 0){
			gameObject.renderer.material = red;
		}
		if (condition <= 0){
			Destroy(gameObject);
		}	
	}
}
