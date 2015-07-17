using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	public KeyCode LEFT_THRUSTER_KEY = KeyCode.A;
	public KeyCode RIGHT_THRUSTER_KEY = KeyCode.D;
	public KeyCode FORWARD_THRUSTER_KEY = KeyCode.W;
	
	protected PlayerController pController;
	
	protected void Awake() {
		pController = GetComponent<PlayerController>();
	}
	
	protected void Update () {
		if (Input.GetKey(LEFT_THRUSTER_KEY)) {
			pController.HandleLeftThrusterPressed();
		}
		
		if (Input.GetKey(RIGHT_THRUSTER_KEY)) {
			pController.HandleRightThrusterPressed();
		}
	}
}
