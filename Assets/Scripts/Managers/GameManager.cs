using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private BabyGrowth[] babyGrowthTiles;

	public int Score { get; private set; }

	private void Awake() {
		babyGrowthTiles = FindObjectsOfType<BabyGrowth>();
		foreach(var tile in babyGrowthTiles) {
			tile.BabyGrowthCompleted += OnBabyGrowthCompleted;
		}
	}

	private void OnBabyGrowthCompleted(object sender, EventArgs args) {
		Score++;
	}
}
