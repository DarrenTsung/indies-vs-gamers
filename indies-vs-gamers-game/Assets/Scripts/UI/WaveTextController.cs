using UnityEngine;
using System.Collections;

public class WaveTextController : MonoBehaviour {
	protected int _cachedWave = -1;
	protected tk2dTextMesh _textMesh;
	protected Animator _animator;
	
	protected void Awake() {
		_textMesh = GetComponent<tk2dTextMesh>();
		_animator = GetComponent<Animator>();
	}

	protected void Update() {
		int wave = EnemyManager.Instance.CurrentWaveIndex;
		if (_cachedWave != wave) {
			_cachedWave = wave;
			_textMesh.text = "WAVE " + (_cachedWave + 1).ToString();
			_animator.SetTrigger("Increment");
		}
	}
}
