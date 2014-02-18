using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour {

	public ForwardInteraction interact;
	public InventoryMain inventory;
	public ItemSelection selected;
	public float weaponRange;
	public float fireAngle;
	public float weaponCooldown;
	public float timeSinceFire;
	public string fireButton;
	public AudioSource weaponSound;

	void Awake () {		
		weaponCooldown = 0.5f;
		timeSinceFire = weaponCooldown;
		inventory = GameObject.Find ("Graphics").GetComponent<InventoryMain>();
		selected = GameObject.Find ("Player").GetComponent<ItemSelection>();
		interact = GetComponent<ForwardInteraction>();
		weaponRange = 1000.0f;
		fireAngle = 30.0f;

	}

	void Update () {
		timeSinceFire += Time.deltaTime * 1;
		if (Input.GetButtonDown("Fire1")) {
			if (selected.selectedWeapon == 2) {
				if (inventory.items[2].carried > 0) {
					fire ();
				}
			}
		}
	}

	void fire () {
		Enemy enemy = interact.getTarget(timeSinceFire, weaponCooldown, "Enemy", weaponRange, fireAngle) as Enemy;
		weaponSound.Play();
		if (enemy != null) {
			if (attackHit(enemy)) {
				applyDamage(enemy);
			}
		}
	}
	
	bool attackHit(Enemy enemy) {
		int attackRoll = Random.Range(1,100);
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
