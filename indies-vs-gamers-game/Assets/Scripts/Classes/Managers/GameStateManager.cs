using DT;
﻿using UnityEngine;
using System.Collections;
using GameJolt.API.Objects;
	
public enum GameState { TITLE_SCREEN, TUTORIAL, GAME, WIN, LOSE };

public class GameStateManager : Singleton<GameStateManager> {
	protected float BUFFER_LENGTH = 2.2f;
	protected float winLoseInputBuffer;
	protected float startGameInputBuffer;
	protected GameObject[] _tutorialObjects;
	protected GameObject[] _titleObjects;
	
	public bool Started {
		get { return _started; }
	}
	protected bool _started = false;
	
	public void WinGame() {
		int scoreValue = GameManager.Instance.Score;
		
		string scoreText;
		if (scoreValue == 1) {
    	scoreText = scoreValue.ToString() + " Destroyed Drone";
		} else {
    	scoreText = scoreValue.ToString() + " Destroyed Drones";
		}
    int tableID = 0; 
    string extraData = ""; // This will not be shown on the website. You can store any information.
		
		GameJolt.UI.Manager.Instance.QueueNotification("Score: " + scoreValue.ToString()); 
    
		winLoseInputBuffer = BUFFER_LENGTH;
		CurrentState = GameState.WIN;
		
    GameJolt.API.Scores.Add(scoreValue, scoreText, tableID, extraData, (bool success) => {
			ShowLeaderboards();
    });
	}
	
	public void ShowLeaderboards() {
		GameJolt.UI.Manager.Instance.ShowLeaderboards();
	}
	
	public void LoseGame() {
		winLoseInputBuffer = BUFFER_LENGTH;
		CurrentState = GameState.LOSE;
	}
	
	public void ResetGame() {
		GameJolt.UI.Manager.Instance.DismissLeaderboard();
		CurrentState = GameState.GAME;
	}
	
	protected GameStateManager() {}
	
	protected GameState _currentState;
	public GameState CurrentState {
		get { return _currentState; }
		set {
			WillTransitionToState(value);
			_currentState = value;
		}
	}
	
	protected void Awake() {
		_tutorialObjects = GameObject.FindGameObjectsWithTag("Tutorial");
		foreach (GameObject obj in _tutorialObjects) {
			obj.SetActive(false);
		}
		
		_titleObjects = GameObject.FindGameObjectsWithTag("TitleScreen");
		foreach (GameObject obj in _titleObjects) {
			obj.SetActive(false);
		}
	}
	
	protected void Start() {
		bool isSignedIn = GameJolt.API.Manager.Instance.CurrentUser != null;
		if (!isSignedIn) {
			ShowSignInScreen();
		} else {
			ShowTitleScreen();
		}
	}
	
	protected void ShowSignInScreen() { 
		GameJolt.UI.Manager.Instance.ShowSignIn(success => {
				if (success) {
					ShowTitleScreen();
				} else {
					ShowSignInScreen();
				}
			});
	}
		
	protected void ShowTitleScreen() {
		_started = true;
		startGameInputBuffer = BUFFER_LENGTH;
		foreach (GameObject obj in _titleObjects) {
			obj.SetActive(true);
		}
		
		CurrentState = GameState.TITLE_SCREEN;
	}
	
	protected void Update() {
		if (!_started) {
			return;
		}
		
		winLoseInputBuffer -= Time.deltaTime;
		startGameInputBuffer -= Time.deltaTime;
		
		if (Input.anyKey) {
			if (CurrentState == GameState.TITLE_SCREEN && startGameInputBuffer <= 0.0f) {
				GameObject[] titleScreenObjects = GameObject.FindGameObjectsWithTag("TitleScreen");
				foreach (GameObject titleScreenObject in titleScreenObjects) {
					Animator titleScreenAnimator = titleScreenObject.GetComponent<Animator>();
					
					if (titleScreenAnimator) {
						titleScreenAnimator.SetTrigger("Disappear");
					} else {
						Debug.LogError("Title screen object - no animator");
					}
				}
				StartGame();
			} else if ((CurrentState == GameState.WIN || CurrentState == GameState.LOSE) && winLoseInputBuffer <= 0.0f) {
				ResetGame();
			}
		} 
	}
		
	protected void StartGame() {
		int firstTime = PlayerPrefs.GetInt("FirstTime", 0);
		if (firstTime == 0 || true) {
			// first time flow
			PlayerPrefs.SetInt("FirstTime", 1);
			CurrentState = GameState.TUTORIAL;
		} else {
			CurrentState = GameState.GAME;
		}
	}
	
	protected void WillTransitionToState(GameState newState) {
		switch (newState) {
			case GameState.TUTORIAL:
				foreach (GameObject obj in _tutorialObjects) {
					obj.SetActive(true);
				}
				goto case GameState.GAME;
			case GameState.GAME:
				// reset game objects
				IGameStateInterface[] interfaces = transform.GetComponentsInChildren<IGameStateInterface>();
				foreach (IGameStateInterface component in interfaces) {
					component.Reset();
				}
				break;
			case GameState.WIN:
				GameObject[] winObjects = GameObject.FindGameObjectsWithTag("Win");
				foreach (GameObject obj in winObjects) {
					Animator animator = obj.GetComponent<Animator>();
					animator.SetTrigger("Reset");
				}
				break;
			case GameState.LOSE:
				GameObject[] loseObjects = GameObject.FindGameObjectsWithTag("Lose");
				foreach (GameObject obj in loseObjects) {
					Animator animator = obj.GetComponent<Animator>();
					animator.SetTrigger("Reset");
				}
				break;
		}
	}
}
