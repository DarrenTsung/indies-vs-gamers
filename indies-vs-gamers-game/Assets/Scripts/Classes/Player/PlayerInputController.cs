using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	protected PlayerController pController;
	
	protected void Awake() {
		pController = GetComponent<PlayerController>();
	}
	
	protected void Update () {
		float hammerAxis = Input.GetAxis("HammerControl");
		pController.HandleCurrentAxisPressed(hammerAxis);
		
		float horizontal = Input.GetAxis("pMovementHorizontal");
		float vertical = Input.GetAxis("pMovementVertical");
		
		pController.HandleMovementAxis(new Vector2(horizontal, vertical));
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameManager.Instance.Score += 1;
		}
	}
}
