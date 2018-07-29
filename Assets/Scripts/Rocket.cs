using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioData;

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 10f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioData = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Thrust();
		Rotate();
	}

	void OnCollisionEnter(Collision collision) {
		switch(collision.gameObject.tag) {
			case "Friendly":
				// do nothing
				print("Ok");
				break;
			case "Finish":
				print("Hit Finish");
				SceneManager.LoadScene(1);
				break;
			default:
				print("Dead");
				SceneManager.LoadScene(0);
				break;
		}
	}

	void Thrust(){
		if(Input.GetKey(KeyCode.Space)){
			rigidBody.AddRelativeForce(Vector3.up * mainThrust);
			if(!audioData.isPlaying)
				audioData.Play();
		} else {
			audioData.Stop();
		}
	}

	void Rotate(){
		rigidBody.freezeRotation = true; // take manual control of rotation
		float rotationThisFrame = rcsThrust * Time.deltaTime;
		if(Input.GetKey(KeyCode.A)){
			transform.Rotate(Vector3.forward * rotationThisFrame);
		} else if(Input.GetKey(KeyCode.D)){
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}
		rigidBody.freezeRotation = false; // resume physics control of rotation
	}

	
}
