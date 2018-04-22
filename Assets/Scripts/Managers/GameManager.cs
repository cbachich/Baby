using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private float startingGameTime = 240f;

	[SerializeField]
	private Dingo dingoPrefab;

	[SerializeField]
	private float dingoSpawnTime = 20f;

	[SerializeField]
	private Vector2 dingoSpawnPoint = new Vector2(6f, 0f);

	private BabyGrowth[] babyGrowthTiles;
	private float timeSinceDingoSpawn = 1000f;

	public int Score { get; private set; }
	public float TimeRemaining { get; private set; }
	public bool IsGameOver { get; private set; }

	private void Awake() {
		babyGrowthTiles = FindObjectsOfType<BabyGrowth>();
		foreach(var tile in babyGrowthTiles) {
			tile.BabyGrowthCompleted += OnBabyGrowthCompleted;
		}
	}

	private void Start() {
		BeginLevel();
	}

	private void Update() {
		TimeRemaining -= Time.deltaTime;
		timeSinceDingoSpawn += Time.deltaTime;

		if(TimeRemaining <= 0f) {
			TimeRemaining = 0f;
			IsGameOver = true;
		}

		if(timeSinceDingoSpawn >= dingoSpawnTime && !IsGameOver) {
			SpawnDingo();
		}
	}

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void BeginLevel() {
		TimeRemaining = startingGameTime;
	}

	private void SpawnDingo() {
		var dingo = GameObject.Instantiate(dingoPrefab, dingoSpawnPoint, Quaternion.identity);
		dingo.BabyCaptured += OnDingoCapturedBaby;
	}

	#region Event Handlers
	private void OnBabyGrowthCompleted(object sender, EventArgs args) {
		Score++;
	}

	private void OnDingoCapturedBaby(object sender, EventArgs args) {
		Debug.Log("dingo ate my baby!");
	}
	#endregion
}
