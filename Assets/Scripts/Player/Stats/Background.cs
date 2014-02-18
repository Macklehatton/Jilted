using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {	
	
	// Skill Proficincies

	public int searchMod;
	public int detectionMod;
	public int stealthMod;
	public int lockpickMod;
	public int survivalMod;
	public int electronicsMod;
	public int mechanicalMod;
	public int tradeMod;

	// Stunts

	// Sneak attack

	// Double tap
	// Spray room
	// Suppressing fire
	// Snipe
	// Shot on the run

	

	public void characterBackground(string backgroundName)		
	{		
		switch(backgroundName)			
		{			
		case "thief":
			searchMod = 1;
			detectionMod = 1;
			stealthMod = 1;
			lockpickMod = 1;
			survivalMod = 0;
			electronicsMod = 1;
			mechanicalMod = 0;
			tradeMod = 0;
			break;
			
		case "scavenger":
			searchMod = 1;
			detectionMod = 0;
			stealthMod = 0;
			lockpickMod = 1;
			survivalMod = 1;
			electronicsMod = 1;
			mechanicalMod = 1;
			tradeMod = 1;
			break;
		
		case "survivalist":
			searchMod = 1;
			detectionMod = 1;
			stealthMod = 1;
			lockpickMod = 0;
			survivalMod = 1;
			electronicsMod = 0;
			mechanicalMod = 1;
			tradeMod = 0;
			break;
		
		case "mercenary":
			searchMod = 0;
			detectionMod = 1;
			stealthMod = 1;
			lockpickMod = 0;
			survivalMod = 1;
			electronicsMod = 0;
			mechanicalMod = 0;
			tradeMod = 0;
			break;

		case "merchant":
			searchMod = 0;
			detectionMod = 0;
			stealthMod = 0;
			lockpickMod = 0;
			survivalMod = 0;
			electronicsMod = 0;
			mechanicalMod = 0;
			tradeMod = 1;
			break;
		
		case "technician":
			searchMod = 0;
			detectionMod = 0;
			stealthMod = 0;
			lockpickMod = 1;
			survivalMod = 0;
			electronicsMod = 1;
			mechanicalMod = 1;
			tradeMod = 0;
			break;
			
		default:			
			this.name = "Citizen";
			searchMod = 0;
			detectionMod = 0;
			stealthMod = 0;
			lockpickMod = 0;
			survivalMod = 0;
			electronicsMod = 0;
			mechanicalMod = 0;
			tradeMod = 0;
			break;			
		}		
	}	
}