using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]


/// <summary>
/// Rigidbody character controller using physics. Might be worse than a character motor, but it took less time to write for the demo.
/// </summary>

public class CharacterControls : MonoBehaviour {
	public AudioSource jump1;
	public AudioSource jump2;
	public AudioSource jump3;
	public AudioSource footstep1;
	public AudioSource footstep2;
	public AudioSource footstep3;
	public AudioSource footstep4;
	List<AudioSource> footsteps = new List<AudioSource>();
	List<AudioSource> stepsNoRepeat = new List<AudioSource>();
	public float speed = 1.5f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 1.0f;
	public bool canJump = true;
	public float jumpHeight = 1.0f;
	Vector3 lastPosition;	
	bool grounded = false;	
	bool crouching = false;
	bool prone = false;
	bool footstepsPlaying = false;
	public float footstepDelay;
	float timeSinceStep;
	int soundToPlay = 0;
	int lastPlayed = 0;


	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
		footstepDelay = 0.5f;
		footsteps.Add(footstep1);
		footsteps.Add(footstep2);
		footsteps.Add(footstep3);
		footsteps.Add(footstep4);		
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

			lastPosition = transform.position;

			//TODO play the sounds randomly from a list rather than switch, simpler (see footsteps)
			if (canJump && Input.GetButtonDown("Jump")) {
				int soundToPlay = Random.Range(1,3);
				switch(soundToPlay) 
				{
				case 1:
					jump1.Play();
					break;
				case 2:
					jump2.Play();					
					break;
				case 3:
					jump3.Play();
					break;
				default:
					break;
				}
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}

			if (Input.GetButton("Crouch")) {
				speed = 2.0f;
				transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				crouching = true;
			} else {
				speed = 3.0f;
				transform.localScale = new Vector3(0.5f, 0.7f, 0.5f);
				crouching = false;
			}

			if (Input.GetButtonDown("Prone")) {
				prone = !prone;
			}

			if (prone == true) {
				speed = 1.0f;
				transform.localScale = new Vector3(0.5f, 0.25f, 0.5f);
			}

		}
		
		// We apply gravity manually for more tuning control
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}

	void Update() {
		// Footsteps
		if (Input.GetAxis("Vertical") > 0.1 && transform.position != lastPosition) {
			if (footstepsPlaying == false) {				
				soundToPlay = Random.Range(0,3);
				if (soundToPlay != lastPlayed) {
					lastPlayed = soundToPlay;
					footsteps[soundToPlay].Play();
					footstepsPlaying = true;
					timeSinceStep = 0;
				} else {
					soundToPlay = Random.Range(0,3);
				}
			}			
			if (timeSinceStep >= footstepDelay) {
				if (   footstep1.isPlaying == false
				    && footstep2.isPlaying == false 
				    && footstep3.isPlaying == false
				    && footstep4.isPlaying == false) {
					footstepsPlaying = false;
				}
			}
		}
		timeSinceStep += Time.deltaTime;
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