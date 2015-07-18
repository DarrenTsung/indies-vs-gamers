using UnityEngine;
using System.Collections;

public class MountainController : MonoBehaviour {
	public float MOUNTAIN_MOVEMENT_MAX = 2.0f;
	public float MOUNTAIN_MOVEMENT_SPEED = 0.1f;

	protected Transform spriteTransform;
	protected float offset;
	
	protected void Awake() {
		spriteTransform = transform.Find("Sprite");
		offset = Random.Range(0.0f, 1.0f);
	}
	
	protected void Update () {
		Vector3 rawPosition = new Vector3(0.0f, Mathf.Sin(Time.fixedTime + offset) * MOUNTAIN_MOVEMENT_MAX / (1.0f / MOUNTAIN_MOVEMENT_SPEED), 0.0f);
		spriteTransform.localPosition = GameManager.Instance.SnapToNearestPixel(rawPosition);
	}
}
