using UnityEngine;
using System.Collections;

public class ApplyToChildren : MonoBehaviour {
	// Use this for initialization
	void Awake () {
		foreach(Transform child in gameObject.transform)
		{
			child.gameObject.AddComponent("FollowTerrainY");
		}
	}
}
