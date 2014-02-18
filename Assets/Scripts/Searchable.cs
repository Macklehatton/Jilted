using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Searchable : MonoBehaviour {

	PlayerMain player;
	List<WorldItem> itemsInRoom = new List<WorldItem>();	
	List<string> itemsFound = new List<string>();
	bool displayItems = false;
	bool searching = false;
	int searchCheck = 0;
	bool searched = false;
	int selected;


	void Awake () {
		player = GameObject.Find("Player").GetComponent<PlayerMain>();
	}

	void Update () {
		if (selected == 0) {

		}
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			displayItems = true;
			searching = true;
		}
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Player") {
			if (searching == true) {
				search ();
				searching = false;
				searched = true;
			}
		}

		if (other.gameObject.GetComponent<WorldItem>()) {
			if (!itemsInRoom.Contains(other.gameObject.GetComponent<WorldItem>())){
				itemsInRoom.Add(other.GetComponent<WorldItem>());
			}
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "Player") {
			displayItems = false;
			searching = false;
		}
	}

	void OnGUI () {
		if (displayItems == true) {
			selected = GUI.SelectionGrid(new Rect(25, 25, 140, 50), itemsFound.Count, itemsFound.ToArray(), itemsFound.Count);
		}		
	}

	void search () {
		searchCheck = Random.Range (1,100) + player.search;
		if (searching == true) {
			if (searched == false) {
				foreach (WorldItem item in itemsInRoom) {
					if (searchCheck > item.searchDifficulty) {
						itemsFound.Add(item.name);
					}
				}
			}
		}
	}
}
