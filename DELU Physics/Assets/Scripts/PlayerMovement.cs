using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityController))]
public class PlayerMovement : MonoBehaviour {

	private Rigidbody body;

	private GravityController grav;

    [SerializeField]
    private float jumpVel = 15f;

	[SerializeField]
	private float minVel = 5f;

	[SerializeField]
	private float maxVel = 10f;
	// Use this for initialization

	private float currentVel = 0f;
	private void Awake() {
		body = GetComponent<Rigidbody>();
		grav = GetComponent<GravityController>();
	}
	void Start () {
		currentVel = Mathf.Lerp(minVel, maxVel, grav.CurrentGravityModifierNormalized);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump")) {
            body.velocity += jumpVel * Vector3.up;
        }
		currentVel = Mathf.Lerp(minVel, maxVel, grav.CurrentGravityModifierNormalized);
		//body.AddForce(currentAcc * Vector3.right, ForceMode.Force);
        body.velocity = new Vector3(currentVel, body.velocity.y, body.velocity.z);
		Debug.Log("Velocity: " + currentVel.ToString());
	}
}
