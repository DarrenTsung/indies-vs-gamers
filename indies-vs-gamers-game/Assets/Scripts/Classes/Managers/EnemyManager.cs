using DT;
﻿using DT.ObjectPools;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager> {
	protected const string ENEMY_2_KEY = "Enemy2";
	protected const string ENEMY_2_MAX_SPEED_KEY = "Enemy2MaxSpeed";
	protected const string TARGET_SCORE_KEY = "TargetScore";
	
	public float ENEMY_2_MAX_SPEED = 4.0f;
	public float ENEMY_2_MIN_SPEED = 2.0f;
	
	protected static List<Dictionary<string, float>> _waveMap =
		new List<Dictionary<string, float>>() {
        new Dictionary<string, float> {
					{ENEMY_2_KEY, 10},
					{ENEMY_2_MAX_SPEED_KEY, 4},
					{TARGET_SCORE_KEY, 1}
				},
        new Dictionary<string, float> {
					{ENEMY_2_KEY, 20},
					{ENEMY_2_MAX_SPEED_KEY, 4.3f},
					{TARGET_SCORE_KEY, 10}
				},
        new Dictionary<string, float> {
					{ENEMY_2_KEY, 35},
					{ENEMY_2_MAX_SPEED_KEY, 5},
					{TARGET_SCORE_KEY, 20}
				},
        new Dictionary<string, float> {
					{ENEMY_2_KEY, 50},
					{ENEMY_2_MAX_SPEED_KEY, 6},
					{TARGET_SCORE_KEY, 50}
				},
        new Dictionary<string, float> {
					{ENEMY_2_KEY, 100},
					{ENEMY_2_MAX_SPEED_KEY, 8},
					{TARGET_SCORE_KEY, 100}
				},
        new Dictionary<string, float> {
					{ENEMY_2_KEY, 200},
					{ENEMY_2_MAX_SPEED_KEY, 10},
					{TARGET_SCORE_KEY, 200}
				},
    };
				
	protected EnemyManager() {}
	
	public GameObject enemy2Template;
	protected bool _spawning;
	
	public int CurrentWaveIndex {
		get { return _currentWaveIndex; }
	}
	protected int _currentWaveIndex;
	protected Dictionary<string, float> _currentWave;
	
	ObjectPool<Enemy2Controller> _enemy2Pool;
	
	public void IncrementWave() {
		StartWave(_currentWaveIndex + 1);
	}
	
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
		
		_currentWaveIndex = index;
		_currentWave = _waveMap[index];
		
		ENEMY_2_MAX_SPEED = _currentWave[ENEMY_2_MAX_SPEED_KEY];
	}
	
	protected void Update() {
		if (_spawning) {
			UpdateWave();
			UpdateSpawning();
		}
	}
	
	protected void UpdateWave() {
		if (GameManager.Instance.Score >= _currentWave[TARGET_SCORE_KEY]) {
			IncrementWave();
		}
	}
	
	protected void UpdateSpawning() {
		if (_currentWave.ContainsKey(ENEMY_2_KEY)) {
			int targetEnemy2s = (int)_currentWave[ENEMY_2_KEY];
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
