using UnityEngine;
using System.Collections;

public class ScoreTextController : MonoBehaviour {
	protected int _cachedScore = -1;
	protected tk2dTextMesh _textMesh;
	protected Animator _animator;
	
	protected void Awake() {
		_textMesh = GetComponent<tk2dTextMesh>();
		_animator = GetComponent<Animator>();
	}

	protected void Update() {
		int score = GameManager.Instance.Score;
		if (_cachedScore != score) {
			_cachedScore = score;
			_textMesh.text = _cachedScore.ToString();
			_animator.SetTrigger("Increment");
		}
	}
}
