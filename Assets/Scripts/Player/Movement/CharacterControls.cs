using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]


/// <summary>
/// Rigidbody character controller using physics. Might be worse than a character motor, but it took less time to write for the demo.
/// </summary>

public class CharacterControls : MonoBehaviour {
	public AudioSource Jump1;
	public AudioSource Jump2;
	public AudioSource Jump3;
	public float speed = 2.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 1.0f;
	public bool canJump = true;
	public float jumpHeight = 1.0f;
	bool grounded = false;	
	bool crouching = false;
	bool prone = false;


	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	void FixedUpdate () {
		if (grounded) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			if (canJump && Input.GetButtonDown("Jump")) {
				int soundToPlay = Random.Range(1,3);
				switch(soundToPlay) 
				{
				case 1:
					Jump1.Play();
					break;
				case 2:
					Jump2.Play();					
					break;
				case 3:
					Jump3.Play();
					break;
				default:
					break;
				}
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}

			if (Input.GetButton("Crouch")) {
				speed = 2.0f;
				transform.localScale = new Vector3(1,0.5f,1);
				crouching = true;
			} else {
				speed = 3.0f;
				transform.localScale = new Vector3(1,1,1);
				crouching = false;
			}

			if (Input.GetButtonDown("Prone")) {
				prone = !prone;
			}

			if (prone == true) {
				speed = 1.0f;
				transform.localScale = new Vector3(1,0.25f,1);
			}

		}
		
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}


	//TODO Make player stop sticking to ceiling and walls
	void OnCollisionStay () {
		grounded = true;    
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}