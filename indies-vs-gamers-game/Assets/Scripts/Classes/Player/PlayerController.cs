using DT.TweakableVariables;
﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected const float PLAYER_LEFT_THRUSTER_SPEED_DEFAULT = 20.0f;
	protected const float PLAYER_RIGHT_THRUSTER_SPEED_DEFAULT = 20.0f;
	protected const float PLAYER_THRUSTER_FORCE_DEFAULT = 800.0f;
	
	protected const float MOTOR_FORWARD_THRUST = 4.0f;
	
	protected TweakableFloat _leftThrusterSpeed;
	protected TweakableFloat _rightThrusterSpeed;
	protected TweakableFloat _thrusterForce;
	
	public float LeftThrusterSpeed {
		get { return _leftThrusterSpeed.Value; }
	}
	
	public float RightThrusterSpeed {
		get { return _rightThrusterSpeed.Value; }
	}
	
	public float ThrusterForce {
		get { return _thrusterForce.Value; }
	}
	
	protected GameObject _characterCenterObj;
	protected GameObject _hammerObj;
	protected Rigidbody2D _rigidbody;
	protected HeatController _heatController;
	
	protected Vector2 _currentMovementAxis;
	float _currentHammerAxis;
	
	public void HandleCurrentAxisPressed(float axis) {
		_currentHammerAxis = axis;
	}
	
	public void HandleMovementAxis(Vector2 movementAxis) {
		_currentMovementAxis = movementAxis;
	}
	
	protected void Awake() {
		_leftThrusterSpeed = new TweakableFloat("_leftThrusterSpeed", 0.0f, 100.0f, PLAYER_LEFT_THRUSTER_SPEED_DEFAULT);
		_rightThrusterSpeed = new TweakableFloat("_rightThrusterSpeed ", 0.0f, 100.0f, PLAYER_RIGHT_THRUSTER_SPEED_DEFAULT);
		_thrusterForce = new TweakableFloat("_thrusterForce", 100.0f, 10000.0f, PLAYER_THRUSTER_FORCE_DEFAULT);
		
		_hammerObj = transform.Find("BiscuitHammer").gameObject;
		_characterCenterObj = transform.Find("CharacterCenter").gameObject;
		_rigidbody = GetComponent<Rigidbody2D>();
		_heatController = GetComponent<HeatController>();
	}
	
	protected void FixedUpdate() {
		UpdateMovement();
		UpdateHammer();
	}
	
	protected void UpdateMovement() {
		_rigidbody.velocity = _currentMovementAxis * ThrusterForce * Time.fixedDeltaTime;
	}
		
	protected void UpdateHammer() {
		if (_heatController.IsOverheated()) {
			return;
		}
		
		float motorSpeed = 0.0f;
		if (_currentHammerAxis < 0.0f) {
			motorSpeed = _currentHammerAxis * LeftThrusterSpeed;
		} else if (_currentHammerAxis > 0.0f) {
			motorSpeed = _currentHammerAxis * RightThrusterSpeed;
		}
		
		_hammerObj.transform.Rotate(new Vector3(0.0f, 0.0f, -motorSpeed));
		_heatController.AddHeat(Mathf.Abs(_currentHammerAxis * Time.deltaTime));
	}
}
