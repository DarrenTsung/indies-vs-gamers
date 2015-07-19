using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionController : MonoBehaviour {
	[SerializeField]
	protected List<ParticleSystem> _particleSystems;

	public void Explode() {
		foreach (ParticleSystem ps in _particleSystems) {
			ps.Emit(1);
		}
	}
}
