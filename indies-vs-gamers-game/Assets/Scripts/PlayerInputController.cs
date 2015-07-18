using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	public KeyCode FORWARD_THRUSTER_KEY = KeyCode.W;
	
	protected PlayerController pController;
	
	protected void Awake() {
		pController = GetComponent<PlayerController>();
	}
	
	protected void Update () {
		float hammerAxis = Input.GetAxis("HammerControl");
		pController.HandleCurrentAxisPressed(hammerAxis);
		
		if (Input.GetKey(FORWARD_THRUSTER_KEY)) {
			pController.HandleForwardThrusterPressed();
		}
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			GameManager.Instance.Score += 1;
		}
	}
}
