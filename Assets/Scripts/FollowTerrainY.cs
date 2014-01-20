using UnityEngine;
using System.Collections;

public class FollowTerrainY : MonoBehaviour {



	// Use this for initialization
	void Start () {
		float newY = Terrain.activeTerrain.SampleHeight(transform.position);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);
	}
}
