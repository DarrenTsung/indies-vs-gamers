using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	protected const float CLOUD_SPEED_MIN = 0.1f;
	protected const float CLOUD_SPEED_MAX = 0.7f;
	
	protected float _speed;
	
	protected void Awake() {
		_speed = Random.Range(CLOUD_SPEED_MIN, CLOUD_SPEED_MAX);
	}
	
	protected void Update() {
		transform.position = transform.position + new Vector3(Time.deltaTime * _speed, 0.0f, 0.0f);
	}
}
