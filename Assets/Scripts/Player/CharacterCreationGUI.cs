using UnityEngine;
using System.Collections;

public class CharacterCreationGUI : MonoBehaviour {
	
	public Background background;
	public PlayerMain player;

	public int buttonH;
	public int buttonW;

	public int backBH;
	public int backBW;

	public int statH;
	public int statW;

	public int border;
	public int spacing;

	public int selGridInt = 0;
	public string[] backgrounds;
	
	void Awake() {
		backBH = 30;
		backBW = 100;
		statH = 25;
		statW = 85;
		border = 15;
		spacing = 5;

		backgrounds = new string[] {"Scavenger", "Technician", "Mercenary", "Thief", "Merchant", "Survivalist"};

	}

	// Use this for initialization
	void Start () {
		
	} 

	void OnGUI() {
		GUI.Box(new Rect(border, border, statW, statH), "Stamina: " + player.stamina.ToString());
		
		//Backgrounds
//		if (GUI.Button(new Rect((Screen.width / 4) + border, border, backBW, backBH), "Thief"))
//		{
//			background.characterBackground("thief");
//		}
//
//		if (GUI.Button(new Rect((Screen.width / 4) + border, backBH + border + spacing, backBW, backBH), "Scavenger"))
//		{
//			background.characterBackground("scavenger");
//		}

		selGridInt = GUI.SelectionGrid(new Rect(Screen.width / 2 - Screen.width / 16, border, Screen.width / 8, Screen.height / 2), selGridInt, backgrounds, 1);



		//Skills

		if (GUI.Button(new Rect(10, 70, 30, 30), "+")){
			player.athletics += 1;}
		if (GUI.Button(new Rect(50, 70, 30, 30), "-")){
			player.athletics -= 1;}
		
	}


	
	// Update is called once per frame
	void Update () {
		switch (selGridInt)
		{
		case 0:
			player.background.characterBackground("scavenger");
			break;
		case 1:
			player.background.characterBackground("technician");
			break;
		case 2:
			player.background.characterBackground("mercenary");
			break;
		case 3:
			player.background.characterBackground("thief");
			break;
		case 4:
			player.background.characterBackground("merchant");
			break;
		case 5:
			player.background.characterBackground("survivalist");
			break;
		}

	}
}
