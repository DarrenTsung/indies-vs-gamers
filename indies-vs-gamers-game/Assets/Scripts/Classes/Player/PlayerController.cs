using DT.TweakableVariables;
﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected const float PLAYER_THRUSTER_FORCE_DEFAULT = 800.0f;
	protected const float MOTOR_FORWARD_THRUST = 4.0f;
	
	protected TweakableFloat _thrusterForce;
	
	public float ThrusterForce {
		get { return _thrusterForce.Value; }
	}
	
	protected GameObject _characterCenterObj;
	protected GameObject _hammerObj;
	protected Rigidbody2D _rigidbody;
	protected HeatController _heatController;
	
	protected Vector2 _currentMovementAxis;
	float _currentHammerAxis;
	
	public void HandleMovementAxis(Vector2 movementAxis) {
		_currentMovementAxis = movementAxis;
	}
	
	protected void Awake() {
		_thrusterForce = new TweakableFloat("_thrusterForce", 800.0f, 10000.0f, PLAYER_THRUSTER_FORCE_DEFAULT);
		
		_hammerObj = transform.Find("BiscuitHammer").gameObject;
		_characterCenterObj = transform.Find("CharacterCenter").gameObject;
		_rigidbody = GetComponent<Rigidbody2D>();
		_heatController = GetComponent<HeatController>();
	}
	
	protected void FixedUpdate() {
		UpdateMovement();
	}
	
	protected void UpdateMovement() {
		_rigidbody.velocity = _currentMovementAxis * ThrusterForce * Time.fixedDeltaTime;
	}
}
