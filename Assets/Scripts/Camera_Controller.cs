using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

	public Transform playerRocket;

	void FixedUpdate(){
		transform.position = new Vector3(playerRocket.position.x, playerRocket.position.y, transform.position.z);
	}
}
