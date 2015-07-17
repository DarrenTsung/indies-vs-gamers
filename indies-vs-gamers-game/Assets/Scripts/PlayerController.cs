using DT.TweakableVariables;
﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected const float PLAYER_LEFT_THRUSTER_FORCE_DEFAULT = 10.0f;
	protected const float PLAYER_RIGHT_THRUSTER_FORCE_DEFAULT = 10.0f;
	protected const float PLAYER_FORWARD_THRUSTER_FORCE_DEFAULT = 15.0f;
	
	protected TweakableFloat _leftThrusterForce;
	protected TweakableFloat _rightThrusterForce;
	protected TweakableFloat _forwardThrusterForce;
	
	public float LeftThrusterForce {
		get { return _leftThrusterForce.Value; }
	}
	
	public float RightThrusterForce {
		get { return _rightThrusterForce.Value; }
	}
	
	public float ForwardThrusterForce {
		get { return _forwardThrusterForce.Value; }
	}
	
	protected GameObject hammerObj;
	protected Rigidbody2D hammerRigidbody;
	
	public void HandleLeftThrusterPressed() {
		hammerRigidbody.AddForce(hammerObj.transform.right * RightThrusterForce);
	}
	
	public void HandleRightThrusterPressed() {
		hammerRigidbody.AddForce(-hammerObj.transform.right * LeftThrusterForce);
	}
	
	public void HandleForwardThrusterPressed() {
		hammerRigidbody.AddForce(hammerObj.transform.up * ForwardThrusterForce);
	}

	protected void Awake() {
		_leftThrusterForce = new TweakableFloat("_leftThrusterForce", 10.0f, 40.0f, PLAYER_LEFT_THRUSTER_FORCE_DEFAULT);
		_rightThrusterForce = new TweakableFloat("_rightThrusterForce", 10.0f, 40.0f, PLAYER_RIGHT_THRUSTER_FORCE_DEFAULT);
		_forwardThrusterForce = new TweakableFloat("_forwardThrusterForce", 10.0f, 40.0f, PLAYER_FORWARD_THRUSTER_FORCE_DEFAULT);
		hammerObj = transform.Find("BiscuitHammer").gameObject;
		hammerRigidbody = hammerObj.GetComponent<Rigidbody2D>();
	}
	
	protected void Update() {
		
	}
}
