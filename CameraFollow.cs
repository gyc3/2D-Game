using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	
	GameObject player;

	private Vector3 offset;

	void Start () {
		
		player = GameObject.Find ("Player");
		offset = transform.position - player.transform.position;
	}
	
	void Update () {

		transform.position = player.transform.position + offset;
	}
}
