using DT;
﻿using DT.ObjectPools;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {
	protected const string ENEMY_2_KEY = "Enemy2";
	protected const string TARGET_SCORE_KEY = "TargetScore";
	
	protected static List<Dictionary<string, int>> _waveMap =
		new List<Dictionary<string, int>>() {
		        new Dictionary<string, int> {
							{ENEMY_2_KEY, 40},
							{TARGET_SCORE_KEY, 50}
						}
		    };
				
	protected EnemyManager() {}
	
	public GameObject enemy2Template;
	protected bool _spawning;
	protected Dictionary<string, int> _currentWave;
	
	ObjectPool<Enemy2Controller> _enemy2Pool;
	
	protected void Awake() {
		Enemy2Controller e2Controller = enemy2Template.GetComponent<Enemy2Controller>();
		_enemy2Pool = new ObjectPool<Enemy2Controller>(gameObject, e2Controller);
		
		_spawning = true;
	}
	
	protected void Start() {
		StartWave(0);
	}
	
	protected void StartWave(int index) {
		if (index < 0 || index > _waveMap.Count) {
			Debug.LogError("StartWave - invalid index");
			return;
		}
		
		_currentWave = _waveMap[index];
	}
	
	protected void Update() {
		if (_spawning) {
			UpdateSpawning();
		}
	}
	
	protected void UpdateSpawning() {
		if (_currentWave.ContainsKey(ENEMY_2_KEY)) {
			int targetEnemy2s = _currentWave[ENEMY_2_KEY];
			int currentEnemy2s = _enemy2Pool.CurrentlyActiveObjects().Count;
			
			for (int i = currentEnemy2s; i < targetEnemy2s; i++) {
				SpawnEnemy2();
			}
		}
	}
	
	protected void SpawnEnemy2() {
		Enemy2Controller controller = _enemy2Pool.GetUnusedObject();
		controller.ResetSelf();
		Vector3 randomPosition = CameraController.MainCameraController().RandomPointJustOutsideOfCamera();
		controller.SetPosition(randomPosition);
	}
}
