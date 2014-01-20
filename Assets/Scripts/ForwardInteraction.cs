using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ForwardInteraction : MonoBehaviour {

	public Component getTarget(float timer, float cooldown, string componentToCheck, float radius, float angle) {
		if (checkCooldown(cooldown, timer)) {
			Component target = findInFront(componentToCheck, radius, angle);
			return target;
		} else { 
			return null;
		}
	}

	Component findInFront(string componentToCheck, float radius, float angle){
		Collider[] colliders = sortByDist(spherecastColliders(radius));

		return checkForComponent(colliders, componentToCheck, angle);
	}
	
	Collider[] spherecastColliders(float radius){
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
		return colliders;
	}

	Collider[] sortByDist(Collider[] colliders) {
		colliders = colliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToArray();
		return colliders;
	}

	Component checkForComponent(Collider[] colliders, string componentToCheck, float angle) {
		foreach (Collider entity in colliders) {
			var target = entity.GetComponent(componentToCheck);					
			if (target){
				Vector3 targetDir = target.transform.position - transform.position;						
				if(calcAngle(targetDir) <= angle){
					if (raycastLOS(targetDir)){
						return target; 
					}break;
				}
			}
		}
		return null;
	}
	
	float calcAngle(Vector3 targetDir) {
		float angle = Vector3.Angle(transform.forward, targetDir);
		return angle;
	}

	bool raycastLOS(Vector3 targetDir){
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Physics.Raycast(transform.position, targetDir, out hit);
		if (hit.collider){
			return true;
		}else {
			return false;
		}
	}

	bool checkCooldown(float cooldown, float timer){
		if (timer > cooldown) {
			return true;
		} else {
			return false;
		}
	}

}
