using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGrowth : MonoBehaviour {

	/* Limiters */

	[SerializeField]
    private float GrowthGoal = 30.0f;

	[SerializeField]
    private float WaterMax = 10.0f;

	[SerializeField]
	private BabyPlant babyPlant;

	[SerializeField]
	private Animator plantDialog;

	[SerializeField]
	private Color[] skinColors;

	/* During Life */

    private float waterLevel = 0.0f;
    private float growthLevel = 0.0f;

	private bool growing = false;

	public enum GrowingState { Dormant, GrowingHealthy, GrowingWilting, GrowingDying, Dead };

	public event EventHandler BabyGrowthCompleted;
	public event EventHandler BabyGrowthDied;

	public GameObject baby;
	
	private GrowingState state = GrowingState.Dormant;

	public GrowingState CurrentState { get { return state; } }

	// Use this for initialization
	void Start () {
		babyPlant.PopOutAnimationCompleted += BabyPlantPopOutCompleted;
		Reset();
	}

    // Update is called once per frame
    void Update () {
		if (!growing) {
			return;
		}

		UpdateTimeDeltas();

		if (growthLevel >= GrowthGoal) {
			PopoutABaby();
			return;
		}
		else if(growthLevel >= (GrowthGoal * 0.66f)) {
			babyPlant.Animator.SetInteger("Stage", 3);
		}
		else if(growthLevel >= (GrowthGoal * 0.33f)) {
			babyPlant.Animator.SetInteger("Stage", 2);
		}
		else {
			babyPlant.Animator.SetInteger("Stage", 1);
		}

		plantDialog.SetFloat("WaterLevel", waterLevel / WaterMax);

		UpdateState();
	}

	public void PlantSeed() {
		if (growing) {
			return;
		}

		growing = true;
		waterLevel = WaterMax;
		ChangeState(GrowingState.GrowingHealthy);
		babyPlant.Show(true);
		babyPlant.SetColor(skinColors[UnityEngine.Random.Range(0, skinColors.Length)]);
	}

	public void FillWater() {
		waterLevel = WaterMax;
	}

	private void PopoutABaby() {
		babyPlant.Animator.SetInteger("Stage", 4);
	}

	public bool IsGrowing() {
		return growing;
	}

	private void UpdateTimeDeltas()
	{
		waterLevel -= Time.deltaTime;

		if (IsHealthy()) {
			growthLevel += Time.deltaTime;
		}
	}

	bool IsHealthy() {
		return waterLevel > (WaterMax * 0.6);
	}

	private void UpdateState() {
		if (IsHealthy()) {
			ChangeState(GrowingState.GrowingHealthy);
		}
		else if (waterLevel > (WaterMax * 0.25)) {
			ChangeState(GrowingState.GrowingWilting);
		}
		else if (waterLevel > 0) {
			ChangeState(GrowingState.GrowingDying);
		}
		else {
			Kill();
		}
	}

	private void ChangeState(GrowingState state)
	{
		if(this.state == state) { return; }

		this.state = state;
	}

	private void Kill() {
		if(BabyGrowthDied != null) { BabyGrowthDied(this, EventArgs.Empty); }
		Reset();
	}

	private void Reset() {
		ChangeState(GrowingState.Dormant);
		waterLevel = WaterMax;
		growthLevel = 0.0f;
		growing = false;
		babyPlant.Show(false);
		babyPlant.Animator.SetInteger("Stage", 1);
		plantDialog.SetFloat("WaterLevel", 1);
	}

    private void BabyPlantPopOutCompleted(object sender, EventArgs e)
    {
		if (BabyGrowthCompleted != null) {
			BabyGrowthCompleted(this, EventArgs.Empty);
		}

		Instantiate(baby, transform.position, transform.rotation);

		Reset();
    }
}
