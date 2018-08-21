using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(GravityController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour {

	private Rigidbody body;

	private GravityController grav;

    private AudioSource bgm;

    [SerializeField]
    private float jumpVel = 15f;

	[SerializeField]
	private float minVel = 5f;

	[SerializeField]
	private float maxVel = 10f;
	// Use this for initialization

    private float oldVel = 0f;
	private float currentVel = 0f;

    [SerializeField]
    private float initialPitch = 1f;

    [SerializeField]
    private float pitchFactor = 1f;

    private float pitchChange = 0f;

	[Header("Ground")]
	[SerializeField]
	private Transform feet;

	[SerializeField]
	private float groundRadius = 2f;

	[SerializeField]
	private LayerMask whatIsGround;

	public bool IsGrounded {get; private set;}

	private bool alive = true;

#if UNITY_EDITOR
	[SerializeField]
	private bool showGroundSphere = false;
#endif // UNITY_EDITOR

	private void Awake() {
		body = GetComponent<Rigidbody>();
		grav = GetComponent<GravityController>();
        bgm = GetComponent<AudioSource>();
	}
	void Start () {
		currentVel = Mathf.Lerp(minVel, maxVel, 1f - grav.CurrentGravityModifierNormalized);
        bgm.pitch = initialPitch;
    }
	
	// Update is called once per frame
	void Update () {
		if (alive)
		{
			CalculateGround();	

			if (IsGrounded) {
				MovementHandler();
			}
		}
        
	}

	void MovementHandler() {
        oldVel = currentVel;
		currentVel = Mathf.Lerp(minVel, maxVel, 1f - grav.CurrentGravityModifierNormalized);
		//body.AddForce(currentAcc * Vector3.right, ForceMode.Force);
        body.velocity = new Vector3(currentVel, body.velocity.y, body.velocity.z);
//		Debug.Log("Velocity: " + currentVel.ToString());

        pitchChange = (currentVel - oldVel)/(maxVel - minVel);
        bgm.pitch += pitchChange * pitchFactor;

		if (Input.GetButtonDown("Jump")) {
            body.velocity += jumpVel * Vector3.up;
        }
	}

	void CalculateGround() {
		IsGrounded = Physics.CheckSphere(feet.position, groundRadius, whatIsGround.value);
	}

	public void OnDeath() {
		alive = false;
	}

	public void OnRevive() {
		alive = true;
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
