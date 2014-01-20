using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class RangedAttack : MonoBehaviour {
	/* On click, gets a target in an angle front of the player and applies damage to them */

	public float radius;
	public float fireAngle;
	public float cooldownTime;
	public float timer;
	public AudioSource weaponSound;

	public Vector3 forward;
	public Vector3 playerPosition;

	void Awake () {
		radius = 1000.0F;
		fireAngle = 30.0F;
		cooldownTime = 0.4F;
		timer = cooldownTime;
	}
	
	void Start () {
	}
	
	void Update () {
		forward = transform.forward;
		playerPosition = transform.position;

		timer += Time.deltaTime;

		if(Input.GetButtonDown("Fire1")){
			shoot();
		}	
	}

	void shoot(){
		if (timer > cooldownTime){
			timer = 0.0F;
			weaponSound.Play();
			Collider[] colliders = spherecastColliders(playerPosition);			
			foreach (Collider entity in colliders) {
				Enemy enemy = entity.GetComponent<Enemy>();				
				if (enemy){
					Vector3 targetDir = enemy.transform.position - playerPosition;					
					if(calcAngle(playerPosition, enemy, forward, targetDir) <= fireAngle){
						if (raycastLOS(playerPosition, targetDir)){
							if (attackHit(enemy) == true){
								applyDamage(enemy);
							}
						}break;
					}
				}
			}
		}
	}
	
	
	Collider[] spherecastColliders(Vector3 playerPosition){
		Collider[] colliders = Physics.OverlapSphere(playerPosition, radius);
		colliders = colliders.OrderBy(x => Vector3.Distance(x.transform.position, playerPosition)).ToArray();
		return colliders;
	}

	// Calculate angle between player's forward facing and a line connecting player and enemy
	float calcAngle(Vector3 playerPosition, Enemy enemy, Vector3 forward, Vector3 targetDir) {
		float angle = Vector3.Angle(forward, targetDir);
		return angle;
	}

	bool raycastLOS(Vector3 playerPosition, Vector3 targetDir){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(playerPosition, targetDir, out hit);

		if (hit.collider){
			return true;
		}
		else {
			return false;
		}
	}

	bool attackHit(Enemy enemy) {
		int attackRoll = Random.Range(1,100);
		Debug.Log(attackRoll);

		if (attackRoll > enemy.deflection){
			return true;
		}else{
			return false;
		}
	}

	void applyDamage (Enemy enemy) {
		int damageRoll = Random.Range(1,100);
		enemy.stamina -=  damageRoll-enemy.damageReduction;
		
		if (damageRoll-enemy.damageReduction > enemy.ballisticThreshold){
			enemy.condition -= (damageRoll-enemy.damageReduction / enemy.ballisticThreshold);
			// Apply threshold effects here
		}
	}
}



