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

	[Header("Ground")]
	[SerializeField]
	private Transform feet;

	[SerializeField]
	private float groundRadius = 2f;

	[SerializeField]
	private LayerMask whatIsGround;

	public bool IsGrounded {get; private set;}

#if UNITY_EDITOR
	[SerializeField]
	private bool showGroundSphere = false;
#endif // UNITY_EDITOR

	private void Awake() {
		body = GetComponent<Rigidbody>();
		grav = GetComponent<GravityController>();
	}
	void Start () {
		currentVel = Mathf.Lerp(minVel, maxVel, 1f - grav.CurrentGravityModifierNormalized);
	}
	
	// Update is called once per frame
	void Update () {
		CalculateGround();	

		if (IsGrounded) {
			MovementHandler();
		}
        
	}

	void MovementHandler() {
		currentVel = Mathf.Lerp(minVel, maxVel, 1f - grav.CurrentGravityModifierNormalized);
		//body.AddForce(currentAcc * Vector3.right, ForceMode.Force);
        body.velocity = new Vector3(currentVel, body.velocity.y, body.velocity.z);
		Debug.Log("Velocity: " + currentVel.ToString());

		if (Input.GetButtonDown("Jump")) {
            body.velocity += jumpVel * Vector3.up;
        }
	}

	void CalculateGround() {
		IsGrounded = Physics.CheckSphere(feet.position, groundRadius, whatIsGround.value);
	}

#if UNITY_EDITOR
	void OnDrawGizmos() {
		if (!showGroundSphere) {
			return;
		}

		if (feet == null) {
			Debug.LogWarning("No feet selected", this);
			return;
		}

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(feet.position, groundRadius);
    }
#endif // UNITY_EDITOR
	
}
