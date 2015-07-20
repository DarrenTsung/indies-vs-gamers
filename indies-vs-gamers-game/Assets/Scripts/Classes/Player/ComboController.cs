using UnityEngine;
using System.Collections;

public class ComboController : MonoBehaviour {
	protected const float COMBO_RESET_BASE_TIME = 1.5f;
	
	public int ComboCount;
	protected float _comboTimer;
	
	public void AddHit() {
		ComboCount++;
		_comboTimer = COMBO_RESET_BASE_TIME;
	}

	protected void Update () {
		_comboTimer -= Time.deltaTime;
		if (_comboTimer <= 0.0f) {
			ComboCount = 0;
		}
	}
}
