using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityController : MonoBehaviour {

	public float CurrentGravityModifier{get; private set;}

	public float CurrentGravityModifierNormalized{get; private set;}

	[SerializeField]
	private float minGrav = 0.3f;

	[SerializeField]
	private float maxGrav = 2f;

	[SerializeField]
	private float gravMaxStep = 0.01f;

	private float gravity = -9.81f;
	// Use this for initialization

	private Vector3 grav;

	private void Awake() {
		gravity = Physics.gravity.y;
		CurrentGravityModifier = 1f;
		CurrentGravityModifierNormalized = (CurrentGravityModifier - minGrav) / (maxGrav - minGrav);
	}
	
	// Update is called once per frame
	void Update () {
		ChangeGravity();
		Debug.Log("Gravity: " + CurrentGravityModifier.ToString());
		Debug.Log("Gravity Normalized: " + CurrentGravityModifierNormalized.ToString());
	}

	void ChangeGravity() {
		float change = gravMaxStep * Input.GetAxis("Vertical") * Time.deltaTime;
		CurrentGravityModifier = Mathf.Clamp(
			CurrentGravityModifier + change, 
			minGrav, 
			maxGrav
		);
		CurrentGravityModifierNormalized = (CurrentGravityModifier - minGrav) / (maxGrav - minGrav);
		grav.y = gravity * CurrentGravityModifier;
		Physics.gravity = grav; 
	}
}
