using DT.ObjectPools;
using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IPoolableObject {
	protected const float MIN_COLLISION_SPEED_TO_DESTROY = 20.0f;
	
	[SerializeField]
	protected bool _beingDestroyed = false;
	[SerializeField]
	protected bool _active = true;
	protected SpriteRenderer[] _spriteRenderers;
	
	public void SetPosition(Vector3 position) {
		transform.position = position;
	}
	
	public virtual void ResetSelf() {
		foreach (SpriteRenderer renderer in _spriteRenderers) {
			renderer.enabled = true;
		}
		
		foreach(Collider2D c in GetComponentsInChildren<Collider2D>()) {
			c.enabled = true;
		}
		
		_beingDestroyed = false;
	}
	
	protected void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag != "Player") {
			return;
		}
		
		DestroySelf(false);
	}
	
	protected void OnCollisionEnter2D(Collision2D coll) {
		if (!_active) {
			return;
		}
		
		if (coll.gameObject.tag != "Player") {
			return;
		}
		
		if (coll.relativeVelocity.magnitude > MIN_COLLISION_SPEED_TO_DESTROY) {
			DestroySelf(false);
		}
	}
	
	protected virtual void Awake() {
		_spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
	}
	
	protected virtual void Start() {
		ResetSelf();
	}
	
	protected virtual void DestroySelf(bool silent) {
		if (_beingDestroyed) {
			return;
		}
		
		_beingDestroyed = true;
		
		foreach (SpriteRenderer renderer in _spriteRenderers) {
			renderer.enabled = false;
		}
		
		foreach (Collider2D c in GetComponentsInChildren<Collider2D>()) {
			c.enabled = false;
		}
		
		if (!silent) {
			ExplosionController explosionController = GetComponent<ExplosionController>();
			if (explosionController) {
				explosionController.Explode();
			}
			
			CameraController c = CameraController.MainCameraController();
			c.Shake(0.7f, 0.2f, 0.03f);
			
			GameManager.Instance.Score += 1;
		}
		
		StartCoroutine(FinishDestruction());
	}
	
	protected IEnumerator FinishDestruction() {
	  yield return new WaitForSeconds(3f);
		_active = false;
		_beingDestroyed = false;
	}
	
	protected virtual void Update() {
		if (CameraController.MainCameraController().IsOutsideGameThreshold(transform.position)) {
			DestroySelf(true);
		}
	}
	
	// PRAGMA MARK - IPoolableObject
	public bool IsActive() {
		return _active;
	}
	
	public void SetActive() {
		_active = true;
	}
}
