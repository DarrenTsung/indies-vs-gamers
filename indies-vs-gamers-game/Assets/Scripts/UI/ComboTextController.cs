using UnityEngine;
using System.Collections;

public class ComboTextController : MonoBehaviour {
	protected int _cachedCombo = -1;
	protected tk2dTextMesh _textMesh;
	protected Animator _animator;
	protected ComboController _comboController;
	
	protected void Awake() {
		_textMesh = GetComponent<tk2dTextMesh>();
		_animator = GetComponent<Animator>();
		
		_comboController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<ComboController>();
	}

	protected void Update() {
		int combo = _comboController.ComboCount;
		if (_cachedCombo != combo) {
			_cachedCombo = combo;
			if (combo > 0) {
				_textMesh.text = "x" + _cachedCombo.ToString();
				_animator.SetTrigger("Increment");
			} else {
				_textMesh.text = "";
			}
		}
	}
}
