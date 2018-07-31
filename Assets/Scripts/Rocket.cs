using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioData;

	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float mainThrust = 10f;

	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip rocketDeath;
	[SerializeField] AudioClip rocketSuccess;

	[SerializeField] ParticleSystem engineParticles;
	[SerializeField] ParticleSystem successParticles;
	[SerializeField] ParticleSystem deathParticles;
	


	enum State { Alive, Dying, Transcending }
	State state = State.Alive;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioData = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		// todo Stop sound on death
		if(state == State.Alive){
			RespondToThrustInput();
			RespondToRotateInput();
		}
	
	}

	private void LoadNextLevel(){
		int currentLevel = SceneManager.GetActiveScene().buildIndex;
		int nextLevel = currentLevel + 1 == SceneManager.sceneCountInBuildSettings ? 0 : currentLevel + 1;
		SceneManager.LoadScene(nextLevel);
	}
	private void LoadFirstLevel(){
		SceneManager.LoadScene(0);
	}

	private void StartSuccessSequence(){
		state = State.Transcending;
		audioData.Stop();
		audioData.PlayOneShot(rocketSuccess);
		successParticles.Play();
		Invoke("LoadNextLevel", 1f);
	}

	private void StartDeathSequence() {
		state = State.Dying;
		audioData.Stop();
		audioData.PlayOneShot(rocketDeath);
		deathParticles.Play();
		Invoke("LoadFirstLevel", 1f);
	}

	void OnCollisionEnter(Collision collision) {
		if(state != State.Alive){
			return;
		}
		switch(collision.gameObject.tag) {	
			case "Friendly":
				// do nothing
				print("Ok");
				break;
			case "Finish":
				StartSuccessSequence();
				break;
			default:
				StartDeathSequence();
				break;
		}
	}

	void RespondToThrustInput(){
		if(Input.GetKey(KeyCode.Space)){
			ApplyThrust();
		} else {
			audioData.Stop();
			engineParticles.Stop();
		}
	}

	private void ApplyThrust(){
		rigidBody.AddRelativeForce(Vector3.up * mainThrust);
		if(!audioData.isPlaying){
			audioData.PlayOneShot(mainEngine);
		}
		engineParticles.Play();
	}

	void RespondToRotateInput(){
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
