using UnityEngine;
using System.Collections;

/// <summary>
/// Makes child objects snap to terrain
/// </summary>

public class ApplyToChildren : MonoBehaviour {
	// Use this for initialization
	void Awake () {
		foreach(Transform child in gameObject.transform)
		{
			child.gameObject.AddComponent("FollowTerrainY");
		}
	}
}
