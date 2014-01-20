using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerMain : MonoBehaviour {


	public CharacterControls characterControls;

	// Flags

	public bool backgroundApplied = false;

	// Attributes

	public int vitality;
	public int agility;
	public int wisdom;

	public int reactionTime;
	public int athletics;
	public int acrobatics;
	public int coordination;
	public int perception;
	public int charisma;	
	
	// Background
	
	public Background background;

	// Stats	

	public string playerName;

	public int stamina;
	public int deflection;
	public int condition;
	public int attackBonus;

	public int ballisticThreshold;
	public int bludgeoningThreshold;
	public int piercingThreshold;
	public int slashingThreshold;

	// Skill proficiencies	
	
	public int search;
	public int detection;
	public int stealth;
	public int lockpick;
	public int survival;	
	public int electronics;
	public int mechanical;
	public int trade;


	// Base variables

	public float baseLockTime;
	public float baseRepairTime;

	public float baseSpeed;
	public float baseJumpHeight;


	// Use this for initialization
	void Awake () {
		baseLockTime = 5.5F;
		baseRepairTime = 10.0F;
		baseSpeed = 5.0F;
		baseJumpHeight = 2.0F;


	}

	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Activate")){
			calcStats();
		}
	}

	public void calcStats(){
		characterControls.speed = athletics + baseSpeed;
		characterControls.jumpHeight = athletics / 2 + baseJumpHeight;
	}
}