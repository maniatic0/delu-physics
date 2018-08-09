using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityController))]
public class PlayerMovement : MonoBehaviour {

	private Rigidbody body;

	private GravityController grav;

	[SerializeField]
	private float minAcc = 5f;

	[SerializeField]
	private float maxAcc = 10f;
	// Use this for initialization

	private float currentAcc = 0f;
	private void Awake() {
		body = GetComponent<Rigidbody>();
		grav = GetComponent<GravityController>();
	}
	void Start () {
		currentAcc = Mathf.Lerp(minAcc, maxAcc, grav.CurrentGravityModifierNormalized);
	}
	
	// Update is called once per frame
	void Update () {
		currentAcc = Mathf.Lerp(minAcc, maxAcc, grav.CurrentGravityModifierNormalized);
		body.AddForce(currentAcc * Vector3.right, ForceMode.Force);
		Debug.Log("Acceleration: " + currentAcc.ToString());
	}
}
