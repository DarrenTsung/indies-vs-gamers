using UnityEngine;
using System.Collections;

public class Enemy2Controller : MonoBehaviour {
	protected const float MAX_SPEED = 4.0f;
	protected const float MIN_SPEED = 2.0f;
	
	// AI : decides a certain direction to go and slowly moves that way
	// if it comes near the player, it will move towards it
	
	protected Vector2 _baseDirection;
	protected float _speed;
	protected Rigidbody2D _rigidbody;
	
	protected void Awake() {
		_baseDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
		_baseDirection.Normalize();
		_speed = Random.Range(MIN_SPEED, MAX_SPEED);
		_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	protected void FixedUpdate() {
		_rigidbody.velocity = _baseDirection * _speed;
	}
}
