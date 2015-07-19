using DT;
using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
	public static Rect GAME_BOUNDS = new Rect(-100.0f, -100.0f, 200.0f, 200.0f);
	
	protected const float PLAYER_OUT_OF_BOUNDS_IMPULSE = 100.0f;
	
	public Vector3 PIXEL_SIZE;
	public int Score;
	
	public Vector3 SnapToNearestPixel(Vector3 input) {
		Vector3 output = new Vector3(0, 0, 0);
		output.x = input.x - (input.x % PIXEL_SIZE.x);
		output.y = input.y - (input.y % PIXEL_SIZE.y);
		output.z = input.z;
		return output;
	}
	
	protected void Awake() {
		PIXEL_SIZE = Camera.main.ScreenToWorldPoint(new Vector3(1, 1, 0)) - Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
	}
	
	protected void FixedUpdate() {
		
	}
}
