using UnityEngine;
using System.Collections;

public class Enemy2Controller : EnemyController {
	
	// AI : decides a certain direction to go and slowly moves that way
	// if it comes near the player, it will move towards it
	
	protected Vector2 _baseDirection;
	protected float _speed;
	protected Rigidbody2D _rigidbody;
	protected ParticleSystem _exhaustParticleSystem;
	
	public override void ResetSelf() {
		base.ResetSelf();
		
		_baseDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		_baseDirection.Normalize();
		_speed = Random.Range(EnemyManager.Instance.ENEMY_2_MIN_SPEED, EnemyManager.Instance.ENEMY_2_MAX_SPEED);
		_rigidbody.velocity = new Vector2(0.0f, 0.0f);
		
		_exhaustParticleSystem.enableEmission = true;
		
		_rigidbody.AddForce(_baseDirection * _speed, ForceMode2D.Impulse);
	}
	
	protected override void Awake() {
		base.Awake();
		_rigidbody = GetComponent<Rigidbody2D>();
		_exhaustParticleSystem = transform.Find("ExhaustParticleSystem").gameObject.GetComponent<ParticleSystem>();
	}
	
	protected override void DestroySelf(bool silent) {
		_exhaustParticleSystem.enableEmission = false;
		
		base.DestroySelf(silent);
	}
}
