using DT.TweakableVariables;
﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IGameStateInterface {
	protected const float PLAYER_THRUSTER_FORCE_DEFAULT = 800.0f;
	protected const float MOTOR_FORWARD_THRUST = 4.0f;
	
	protected const float INJURED_MAX_EMISSION_RATE = 10.0f;
	
	protected TweakableFloat _thrusterForce;
	
	public float ThrusterForce {
		get { return _thrusterForce.Value; }
	}
	
	protected GameObject _characterCenterObj;
	protected GameObject _hammerObj;
	protected Rigidbody2D _rigidbody;
	protected HeatController _heatController;
	protected ParticleSystem _injuredParticleSystem;
	
	protected Vector2 _currentMovementAxis;
	protected float _currentHammerAxis;
	protected bool _active = true;
	
	protected Vector3 _basePosition;
	
	public void HandleMovementAxis(Vector2 movementAxis) {
		_currentMovementAxis = movementAxis;
	}
	
	protected void Awake() {
		_basePosition = transform.position;
		
		_thrusterForce = new TweakableFloat("_thrusterForce", 800.0f, 10000.0f, PLAYER_THRUSTER_FORCE_DEFAULT);
		
		_hammerObj = transform.Find("BiscuitHammer").gameObject;
		_characterCenterObj = transform.Find("CharacterCenter").gameObject;
		_rigidbody = GetComponent<Rigidbody2D>();
		_heatController = GetComponent<HeatController>();
		
		_injuredParticleSystem = transform.Find("PInjuredParticleSystem").gameObject.GetComponent<ParticleSystem>();
	}
	
	protected void FixedUpdate() {
		if (!GameStateManager.Instance.Started) {
			transform.position = _basePosition;
		}
		
		if (!_active) {
			return;
		}
		
		UpdateMovement();
		UpdateInjuredParticleSystem();
	}
	
	protected void UpdateMovement() {
		_rigidbody.velocity = _currentMovementAxis * ThrusterForce * Time.fixedDeltaTime;
	}
	
	protected void UpdateInjuredParticleSystem() {
		_injuredParticleSystem.emissionRate = _heatController.HeatPercentage * INJURED_MAX_EMISSION_RATE;
	}
	
	protected void OnTriggerEnter2D(Collider2D coll) {
		if (!_active) {
			return;
		}
		
		CameraController c = CameraController.MainCameraController();
		c.Shake(1.0f, 0.5f, 0.03f);
		
		_heatController.AddHeat(0.25f);
		
		if (_heatController.IsOverheated()) {
			DestroySelf();
		}
	}
	
	protected void DestroySelf() {
		if (!_active) {
			return;
		}
		
		CameraController c = CameraController.MainCameraController();
		c.Shake(1.7f, 3.0f, 0.03f);
		
		ParticleSystem ps = transform.Find("PExplosionParticleSystem").gameObject.GetComponent<ParticleSystem>();
		ps.Play();
		
		_active = false;
		
		StartCoroutine(FinishDestruction());
	}
	
	protected IEnumerator FinishDestruction() {
	  yield return new WaitForSeconds(2.0f);
		
		GameStateManager.Instance.WinGame();
		
		/*
		SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer renderer in spriteRenderers) {
			renderer.enabled = false;
		}
		*/
	} 
	
	// PRAGMA MARK - IGameStateInterface
	public void Reset() {
		_heatController.Reset();
		
		ParticleSystem ps = transform.Find("PExplosionParticleSystem").gameObject.GetComponent<ParticleSystem>();
		ps.Stop();
		
		_active = true;
		
		_rigidbody.velocity = new Vector2(0.0f, 0.0f);
		
		transform.position = _basePosition;
	}
}
