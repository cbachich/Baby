using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	[SerializeField]
	private float startingGameTime = 240f;

	private BabyGrowth[] babyGrowthTiles;
	private StorkMovement[] storkMovements;

	public int Score { get; private set; }
	public float TimeRemaining { get; private set; }
	public bool IsGameOver { get; private set; }

	private void Awake() {
		babyGrowthTiles = FindObjectsOfType<BabyGrowth>();
		foreach(var tile in babyGrowthTiles) {
			tile.BabyGrowthCompleted += OnBabyGrowthCompleted;
		}

		storkMovements = FindObjectsOfType<StorkMovement>();
		foreach(var stork in storkMovements) {
			stork.StorkCollected += OnStorkCollected;
		}
	}

	private void Start() {
		BeginLevel();
	}

	private void Update() {
		TimeRemaining -= Time.deltaTime;

		if(TimeRemaining <= 0f) {
			TimeRemaining = 0f;
			IsGameOver = true;
		}
	}

	private void OnBabyGrowthCompleted(object sender, EventArgs args) {
		Score += 10;
	}

	private void OnStorkCollected(object sender, EventArgs args)
	{
		// TODO - How do I get this to read the Number of babies collected?
		/*
		GameObject senderGameObject = (GameObject) sender;
		StorkInteract storkInteract = senderGameObject.GetComponent<StorkInteract>();
		int numberOfBabiesCollected = storkInteract.NumberOfBabiesCollected;
		*/

		int numberOfBabiesCollected = 1;

		int multiplier = 1;

		if (numberOfBabiesCollected > 2) {
			multiplier = 2;
		}
		else if (numberOfBabiesCollected > 5) {
			multiplier = 3;
		}
		else if (numberOfBabiesCollected > 10) {
			multiplier = 4;
		}

		Score += (50 * numberOfBabiesCollected) * multiplier;
	}

	public void RestartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void BeginLevel() {
		TimeRemaining = startingGameTime;
	}
}
