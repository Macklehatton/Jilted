using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

/// <summary>
/// This mess is how we add items to the players inventory.
/// </summary>

public class InventoryMain : MonoBehaviour {

	public float pickUpRange;
	public float pickUpAngle;
	public ForwardInteraction interact;
	public Dictionary<int, Item> items = new Dictionary<int, Item>();

	public int itemStacks = 0;
	public int displayHeight = 0;

	public string[] itemsList;

	void Awake () {
		interact = transform.parent.GetComponent<ForwardInteraction>();
		pickUpRange = 2.0f;
		pickUpAngle = 30.0f;

		Item fuel = new Item(0, "Fuel can", "A metal jerrycan of fuel, 20 liters", 20.0f, 20000.0f);
		items.Add(fuel.ID, fuel);
		Item crowbar = new Item(1, "Crowbar", "A metal crowbar", 0.72f ,360.0f);
		items.Add(crowbar.ID, crowbar);
		Item m1911 = new Item(2, "M1911", "A M1911 pistol", 0.72f ,360.0f);
		items.Add(m1911.ID, m1911);

		itemsList = items.Values.Select(k => k.name).ToArray();
	}

	void Update () {
		if (Input.GetButtonDown("Activate")) {
			pickUp();
		}
	}

	void OnGUI () {	
		if (itemStacks > 0) {
			GUI.SelectionGrid(new Rect(25, 25, 140, 50), itemStacks, itemsList, 2);
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

	void pickUp () {
		WorldItem itemOnGround = interact.getTarget(1, 0, "WorldItem", pickUpRange, pickUpAngle) as WorldItem;
		if (itemOnGround != null) {
			items[itemOnGround.ID].carried += 1;
			Destroy(itemOnGround.gameObject);
			itemStacks += 1;
		}
	}

	void sortItemList () {
		foreach (string entity in itemsList) {

		}
	}
}
