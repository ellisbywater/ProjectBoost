using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

	[SerializeField] Vector3 movementVector = new Vector3(2f, 2f, 2f);
	[SerializeField] float period = 2;

	// todo move from inspector later
	[Range(0,1)][SerializeField] float movementFactor; // 0 for not moved, 1 for moved

	Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		
		startingPosition = transform.position;
		Debug.Log("Starting Position::  " + startingPosition);
	}
	
	// Update is called once per frame
	void Update () {

		float cycles = Time.time / period;
		const float tau = Mathf.PI * 2;
		float rawSinWave = Mathf.Sin(cycles * tau);

		Debug.Log("Raw Sin Wave:: " + rawSinWave);
		movementFactor = rawSinWave /2f + 0.5f;
		Vector3 offset = movementVector * movementFactor;
		Debug.Log("Offset::  " + offset);
		transform.position = startingPosition + offset;
	}
}
