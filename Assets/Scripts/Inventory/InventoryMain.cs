using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class InventoryMain : MonoBehaviour {

	public float pickUpRange;
	public float pickUpAngle;
	public ForwardInteraction interact;
	public Dictionary<int, Item> items = new Dictionary<int, Item>();


	void Awake () {
		interact = GetComponent<ForwardInteraction>();
		pickUpRange = 5.0f;
		pickUpAngle = 30.0f;

		Item fuel = new Item(0, "Fuel can", "A metal jerrycan of fuel, 20 liters", 20000.0F, 20.0F);
		items.Add(fuel.ID, fuel);
	}

	void Update() {
		if (Input.GetButtonDown("Activate")) {
			pickUp();
		}
	}

	// All units are metric. Volume in cm, weight in kg

	public class Item {

		public int ID;
		public string name;
		public string description;
		public float weight;
		public float volume;
		public int carried;

		public Item(int ID, string name, string description, float weight, float volume) {
			this.ID = ID;
			this.name = name;
			this.description = description;
			this.weight = weight;
			this.volume = volume;
			this.carried = 0;
		}		
	}

	public void pickUp() {
		WorldItem itemOnGround = interact.getTarget(1, 0, "WorldItem", pickUpRange, pickUpAngle) as WorldItem;
		if (itemOnGround != null) {
			items[itemOnGround.ID].carried += 1;
			Destroy(itemOnGround.gameObject);
		}
	}
	
	

	//Item list




	
}
