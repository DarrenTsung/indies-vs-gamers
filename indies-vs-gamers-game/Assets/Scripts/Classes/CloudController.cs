using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour {
	protected const float CLOUD_SPEED_MIN = 0.1f;
	protected const float CLOUD_SPEED_MAX = 0.7f;
	
	protected float _speed;
	protected Vector3 _basePosition;
	
	protected void Awake() {
		_speed = Random.Range(CLOUD_SPEED_MIN, CLOUD_SPEED_MAX);
		_basePosition = transform.position;
	}
	
	protected void Update() {
		Vector3 rawNewPosition = _basePosition + new Vector3(Time.fixedTime * _speed, 0.0f, 0.0f);
		transform.position = GameManager.Instance.SnapToNearestPixel(rawNewPosition);
	}
}
