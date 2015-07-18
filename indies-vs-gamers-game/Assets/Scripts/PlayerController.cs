using DT.TweakableVariables;
﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected const float PLAYER_LEFT_THRUSTER_SPEED_DEFAULT = 30.0f;
	protected const float PLAYER_RIGHT_THRUSTER_SPEED_DEFAULT = 30.0f;
	protected const float PLAYER_FORWARD_THRUSTER_FORCE_DEFAULT = 10.0f;
	
	protected const float MOTOR_FORWARD_THRUST = 4.0f;
	
	protected TweakableFloat _leftThrusterSpeed;
	protected TweakableFloat _rightThrusterSpeed;
	protected TweakableFloat _forwardThrusterForce;
	
	public float LeftThrusterSpeed {
		get { return _leftThrusterSpeed.Value; }
	}
	
	public float RightThrusterSpeed {
		get { return _rightThrusterSpeed.Value; }
	}
	
	public float ForwardThrusterForce {
		get { return _forwardThrusterForce.Value; }
	}
	
	protected GameObject _characterCenterObj;
	protected GameObject _hammerObj;
	protected Rigidbody2D _rigidBody;
	protected HingeJoint2D _pHingeJoint;
	
	float _currentHammerAxis;
	
	public void HandleCurrentAxisPressed(float axis) {
		_currentHammerAxis = axis;
	}
	
	public void HandleForwardThrusterPressed() {
		ForwardThrust(ForwardThrusterForce);
	}
	
	protected void ForwardThrust(float force) {
		Vector3 upForce = _hammerObj.transform.up;
		_rigidBody.AddForce(-upForce * force);
	}

	protected void Awake() {
		_leftThrusterSpeed = new TweakableFloat("_leftThrusterSpeed", 0.0f, 100.0f, PLAYER_LEFT_THRUSTER_SPEED_DEFAULT);
		_rightThrusterSpeed = new TweakableFloat("_rightThrusterSpeed ", 0.0f, 100.0f, PLAYER_RIGHT_THRUSTER_SPEED_DEFAULT);
		_forwardThrusterForce = new TweakableFloat("_forwardThrusterSpeed", 10.0f, 100.0f, PLAYER_FORWARD_THRUSTER_FORCE_DEFAULT);
		
		_hammerObj = transform.Find("BiscuitHammer").gameObject;
		_characterCenterObj = transform.Find("CharacterCenter").gameObject;
		_rigidBody = GetComponent<Rigidbody2D>();
	}
	
	protected void FixedUpdate() {
		float motorSpeed = 0.0f;
		if (_currentHammerAxis < 0.0f) {
			motorSpeed = _currentHammerAxis * LeftThrusterSpeed;
			
			float percent = LeftThrusterSpeed / PLAYER_LEFT_THRUSTER_SPEED_DEFAULT;
			ForwardThrust(percent * MOTOR_FORWARD_THRUST);
		} else if (_currentHammerAxis > 0.0f) {
			motorSpeed = _currentHammerAxis * RightThrusterSpeed;
			
			float percent = RightThrusterSpeed / PLAYER_RIGHT_THRUSTER_SPEED_DEFAULT;
			ForwardThrust(percent * MOTOR_FORWARD_THRUST);
		}
		
		_hammerObj.transform.Rotate(new Vector3(0.0f, 0.0f, -motorSpeed));
	}
}
