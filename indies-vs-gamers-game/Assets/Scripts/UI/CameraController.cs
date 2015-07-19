using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	protected const float OUT_OF_BOUNDS_FORCE = 200.0f;
	protected const float JUST_OUTSIDE_OFFSET = 2.0f;
	protected const float FURTHER_OUTSIDE_OFFSET = 4.0f;
	
	protected const float speed = 1.5f; 
	
	public static CameraController MainCameraController() {
		return Camera.main.GetComponent<CameraController>();
	}

	protected GameObject _target;
	protected tk2dCamera _camera;
	protected EdgeCollider2D _boundsCollider;
	
	public void Shake(float magnitude, float duration, float timeBetweenShakes) {
		ShakeController shakeController = GetComponentInParent<ShakeController>();
		if (shakeController) {
			shakeController.Shake(magnitude, duration, timeBetweenShakes);
		}
	}

	public void SetTarget(GameObject target) {
		_target = target;
	}
	
	public Vector3 RandomPointJustOutsideOfCamera() {
		Rect cameraExtents = _camera.ScreenExtents;
		
		float xMin = cameraExtents.xMin - JUST_OUTSIDE_OFFSET;
		float xMax = cameraExtents.xMax + JUST_OUTSIDE_OFFSET;
		float yMin = cameraExtents.yMin - JUST_OUTSIDE_OFFSET;
		float yMax = cameraExtents.yMax + JUST_OUTSIDE_OFFSET;
		
		float xDiff = xMax - xMin;
		float yDiff = yMax - yMin;
		float generatedValue = Random.Range(0.0f, 2.0f * xDiff + 2.0f * yDiff);
		
		bool flipped = false;
		float generatedX = xMin, generatedY = yMin; 
		
	 	float xyCombined = xDiff + yDiff;
		if (generatedValue > xyCombined) {
			generatedValue -= xyCombined;
			flipped = true;
		}
		
		if (generatedValue <= xDiff) {
			generatedX += generatedValue;
		}
		
		generatedValue -= xDiff;
		if (generatedValue >= 0.0f) {
			generatedY += generatedValue;
		}
		
		if (!flipped) {
			return new Vector3(generatedX, generatedY, 0.0f);
		} else {
			return new Vector3(-generatedX, -generatedY, 0.0f);
		}
	}
	
	public bool IsOutsideGameThreshold(Vector3 point) {
		Rect cameraExtents = _camera.ScreenExtents;
		
		Rect extendedExtents = new Rect(cameraExtents.xMin - FURTHER_OUTSIDE_OFFSET, 
																		cameraExtents.yMin - FURTHER_OUTSIDE_OFFSET, 
																		cameraExtents.width + 2.0f * FURTHER_OUTSIDE_OFFSET, 
																		cameraExtents.height + 2.0f * FURTHER_OUTSIDE_OFFSET);
		
		Vector2[] newBounds = new Vector2[5];
		newBounds[0] = new Vector2(extendedExtents.xMax, extendedExtents.yMax);
		newBounds[1] = new Vector2(extendedExtents.xMin, extendedExtents.yMax);
		newBounds[2] = new Vector2(extendedExtents.xMin, extendedExtents.yMin);
		newBounds[3] = new Vector2(extendedExtents.xMax, extendedExtents.yMin);
		newBounds[4] = new Vector2(extendedExtents.xMax, extendedExtents.yMax);
		
		_boundsCollider.points = newBounds;
		
		return !extendedExtents.Contains(point);
	}

	protected void Awake () {
		_camera = GetComponent<tk2dCamera>();
	}

	protected void Start() {
    SetTarget(GameObject.FindGameObjectsWithTag("Player")[0]);
    _boundsCollider = transform.Find("Bounds").gameObject.GetComponent<EdgeCollider2D>();
	}

	protected void Update () {
		//Vector3 targetPosition = _target.transform.position;
		//targetPosition.z = transform.position.z; // don't move in the z axis
		
		//transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

		//RestrictCameraInBounds(GameManager.GAME_BOUNDS, new Vector3(0.0f, 0.0f, 0.0f));
		SnapPositionToNearestPixel();
		//UpdateBounds();
	}

	protected bool RestrictCameraInBounds(Rect otherBounds, Vector3 otherPosition) {
		Rect cameraExtents = _camera.ScreenExtents;

		float yOver = (transform.position.y + cameraExtents.yMax) - (otherPosition.y + otherBounds.yMax);
		if (yOver > 0) {
			transform.position = transform.position - new Vector3(0.0f, yOver);
		}

		float yBelow = (transform.position.y + cameraExtents.yMin) - (otherPosition.y + otherBounds.yMin);
		if (yBelow < 0) {
			transform.position = transform.position - new Vector3(0.0f, yBelow);
		}

		float xOver = (transform.position.x + cameraExtents.xMax) - (otherPosition.x + otherBounds.xMax);
		if (xOver > 0) {
			transform.position = transform.position - new Vector3(xOver, 0.0f);
		}

		float xBelow = (transform.position.x + cameraExtents.xMin) - (otherPosition.x + otherBounds.xMin);
		if (xBelow < 0) {
			transform.position = transform.position - new Vector3(xBelow, 0.0f);
		}

		return true;
	}
	
	// protected void UpdateBounds() {
	// 	Rect cameraExtents = _camera.ScreenExtents;
	// 	
	// 	Vector2[] newBounds = new Vector2[5];
	// 	newBounds[0] = new Vector2(cameraExtents.xMax, cameraExtents.yMax);
	// 	newBounds[1] = new Vector2(-cameraExtents.xMax, cameraExtents.yMax);
	// 	newBounds[2] = new Vector2(-cameraExtents.xMax, -cameraExtents.yMax);
	// 	newBounds[3] = new Vector2(cameraExtents.xMax, -cameraExtents.yMax);
	// 	newBounds[4] = new Vector2(cameraExtents.xMax, cameraExtents.yMax);
	// 	
	// 	_boundsCollider.points = newBounds;
	// }
	
	// protected void OnTriggerEnter2D(Collider2D other) {
	// 	Rect cameraExtents = _camera.ScreenExtents;
	// 	Transform otherTransform = other.transform;
	// 	
	// 	float distanceFromLeft = Mathf.Abs(otherTransform.position.x - cameraExtents.xMin);
	// 	float distanceFromRight = Mathf.Abs(otherTransform.position.x - cameraExtents.xMax);
	// 	float distanceFromBottom = Mathf.Abs(otherTransform.position.y - cameraExtents.yMin);
	// 	float distanceFromTop = Mathf.Abs(otherTransform.position.y - cameraExtents.yMax);
	// 	
	// 	float minDistance = Mathf.Min(Mathf.Min(Mathf.Min(distanceFromRight, distanceFromLeft), distanceFromTop), distanceFromBottom);
	// 	
	// 	Rigidbody2D otherRigidbody = other.attachedRigidbody;
	// 	if (minDistance == distanceFromLeft) {
	// 		otherRigidbody.AddForce(new Vector2(OUT_OF_BOUNDS_FORCE, 0.0f));
	// 	} else if (minDistance == distanceFromRight) {
	// 		otherRigidbody.AddForce(new Vector2(-OUT_OF_BOUNDS_FORCE, 0.0f));
	// 	} else if (minDistance == distanceFromBottom) {
	// 		otherRigidbody.AddForce(new Vector2(0.0f, OUT_OF_BOUNDS_FORCE));
	// 	} else if (minDistance == distanceFromTop) {
	// 		otherRigidbody.AddForce(new Vector2(0.0f, -OUT_OF_BOUNDS_FORCE));
	// 	}
	// }
	
	protected void SnapPositionToNearestPixel() {
		Vector3 newPosition = GameManager.Instance.SnapToNearestPixel(transform.position);
		transform.position = newPosition;
	}
}
