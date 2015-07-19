using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	protected PlayerController pController;
	
	protected void Awake() {
		pController = GetComponent<PlayerController>();
	}
	
	protected void Update () {
		float horizontal = Input.GetAxis("pMovementHorizontal");
		float vertical = Input.GetAxis("pMovementVertical");
		
		pController.HandleMovementAxis(new Vector2(horizontal, vertical));
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameManager.Instance.Score += 1;
		}
		
		if (Input.GetKeyDown(KeyCode.J)) {
			CameraController c = Camera.main.GetComponent<CameraController>();
			c.Shake(0.7f, 0.2f, 0.03f);
		}
	}
}
