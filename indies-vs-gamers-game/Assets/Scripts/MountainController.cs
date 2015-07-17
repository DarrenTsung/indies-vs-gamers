using UnityEngine;
using System.Collections;

public class MountainController : MonoBehaviour {
	protected const float MOUNTAIN_MOVEMENT_MAX = 2.0f;
	protected const float MOUNTAIN_MOVEMENT_SPEED = 0.1f;

	protected Transform spriteTransform;
	
	protected void Awake() {
		spriteTransform = transform.Find("Sprite");
	}
	
	protected void Update () {
		spriteTransform.localPosition = new Vector3(0.0f, Mathf.Sin(Time.fixedTime) * MOUNTAIN_MOVEMENT_MAX / (1.0f / MOUNTAIN_MOVEMENT_SPEED), 0.0f);
	}
}
