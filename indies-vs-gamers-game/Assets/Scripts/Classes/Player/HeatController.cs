using UnityEngine;
using System.Collections;

public class HeatController : MonoBehaviour {
	protected const float HEAT_DELAY = 1.5f;
	protected const int HEAT_MAX = 200;
	protected const float HEAT_INCREMENT_AMOUNT = 50.0f;
	protected const float HEAT_DECREMENT_AMOUNT = 100.0f;
	
	public float HeatPercentage {
		get { return _heat / HEAT_MAX; }
	}
	
	[SerializeField]
	protected float _heat;
	protected float _heatDelayTimer;
	
	public bool IsOverheated() {
		return _heat >= HEAT_MAX;
	}

	public void AddHeat(float percentage) {
		if (percentage > 0.0f && _heat < HEAT_MAX) {
			_heat += HEAT_MAX * percentage;
			if (_heat > HEAT_MAX) {
				_heat = HEAT_MAX;
			}
			_heatDelayTimer = HEAT_DELAY;
		}
	}
	
	protected void Update() {
		// _heatDelayTimer -= Time.deltaTime;
		// if (_heatDelayTimer <= 0.0f) {
		// 	_heat -= HEAT_DECREMENT_AMOUNT * Time.deltaTime;
		// 	if (_heat < 0.0f) {
		// 		_heat = 0.0f;
		// 	}
		// }
	}
}
