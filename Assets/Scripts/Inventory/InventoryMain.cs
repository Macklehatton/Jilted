using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class InventoryMain : MonoBehaviour {

	void Update() {

	}


	// All units are metric. Volume in cm, weight in kg

	public class Item {

		public int itemID;
		public string name;
		public string description;
		public float weight;
		public float volume;

		public Item(int itemID, string name, string description, float weight, float volume) {
			this.itemID = itemID;
			this.name = name;
			this.description = description;
			this.weight = weight;
			this.volume = volume;
		}
	}

	public Dictionary<int, Item> items = new Dictionary<int, Item>();

	public void pickUp(Item item) {
		items.Add(item.itemID, item);
	}
	
	

	//Item list

	Item fuel = new Item(000, "Fuel can", "A metal jerrycan of fuel, 20 liters", 20000.0F, 20.0F);


	
}
