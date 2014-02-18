using UnityEngine;
using System.Collections;

public class ItemSelection : MonoBehaviour {

	public int selectedWeapon;
	public InventoryMain inventory;

	GameObject crowbar;
	GameObject m1911;

	void Awake () {
		inventory = GameObject.Find ("Camera").GetComponent<InventoryMain>();

		crowbar = GameObject.Find("Crowbar View Model");
		m1911 = GameObject.Find("M1911 View Model");
	}

	void Update () {
		if (Input.GetButtonDown("Weapon Select 1")) {
			selectedWeapon = 1;
			switchViewModel ();
		}
		if (Input.GetButtonDown("Weapon Select 2")) {
			selectedWeapon = 2;
			switchViewModel ();
		}
		if (Input.GetButtonDown("Weapon Select 3")) {
			selectedWeapon = 3;
			switchViewModel ();
		}
		if (Input.GetButtonDown("Weapon Select 3")) {
			selectedWeapon = 3;
			switchViewModel ();
		}
		if (Input.GetButtonDown("Weapon Select 4")) {
			selectedWeapon = 4;
			switchViewModel ();
		}
	}

	void switchViewModel () {		
		if (selectedWeapon == 1) {
			if (inventory.items[1].carried > 0) {
				crowbar.renderer.enabled = true;
			}
		} else {
			crowbar.renderer.enabled = false;
		}

		if (selectedWeapon == 2) {
			if (inventory.items[2].carried > 0) {
				Renderer[] renderers = m1911.GetComponentsInChildren<Renderer>();				
				foreach (Renderer r in renderers) {
					r.enabled = true;
				}
			}
		} else {
			Renderer[] renderers = m1911.GetComponentsInChildren<Renderer>();			
			foreach (Renderer r in renderers) {
				r.enabled = false;
			}
		}
	}
}
