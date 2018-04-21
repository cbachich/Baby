using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyGrowth : MonoBehaviour {

	/* Limiters */

	[SerializeField]
    private float GrowthGoal = 30.0f;

	[SerializeField]
    private float WaterMax = 10.0f;

	/* During Life */

    private float waterLevel = 0.0f;
    private float growthLevel = 0.0f;

	private bool growing = false;

	public enum GrowingState { Dormant, GrowingHealthy, GrowingWilting, GrowingDying, Dead };
	private GrowingState state = GrowingState.Dormant;

	public GrowingState CurrentState { get { return state; } }

	// Use this for initialization
	void Start () {
		this.ChangeState(GrowingState.Dormant);
		this.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		this.TestInputs();

		if (!this.growing) {
			return;
		}

		this.UpdateTimeDeltas();

		if (this.growthLevel >= this.GrowthGoal) {
			this.PopoutABaby();
			return;
		}

		this.UpdateState();
	}

	private void TestInputs() {
		if (Input.GetButton("Fire1")) {
			this.PlantSeed();
		}

		if (Input.GetButton("Fire2")) {
			this.FillWater();
		}
	}

	public void PlantSeed() {
		if (this.growing) {
			return;
		}

		this.growing = true;
		this.waterLevel = this.WaterMax;
		this.ChangeState(GrowingState.GrowingHealthy);
	}

	public void FillWater() {
		this.waterLevel = this.WaterMax;
	}

	private void PopoutABaby() {
		// TODO - Create a baby object
		GetComponent<SpriteRenderer>().color = Color.yellow;

		//this.ChangeState(State.Dormant);
		this.Reset();
	}

	public bool IsGrowing() {
		return this.growing;
	}

	private void UpdateTimeDeltas()
	{
		this.waterLevel -= Time.deltaTime;
		this.growthLevel += Time.deltaTime;
	}

	private void UpdateState() {
		if (this.waterLevel > (this.WaterMax * 0.6)) {
			this.ChangeState(GrowingState.GrowingHealthy);
		}
		else if (this.waterLevel > (this.WaterMax * 0.25)) {
			this.ChangeState(GrowingState.GrowingWilting);
		}
		else if (this.waterLevel > 0) {
			this.ChangeState(GrowingState.GrowingDying);
		}
		else {
			this.Kill();
		}
	}

	private void ChangeState(GrowingState state)
	{
		this.state = state;

		switch (this.state)
		{
			case GrowingState.Dormant:
				GetComponent<SpriteRenderer>().color = Color.magenta;
				break;
			case GrowingState.GrowingHealthy:
				GetComponent<SpriteRenderer>().color = Color.green;
				break;
			case GrowingState.GrowingWilting:
				GetComponent<SpriteRenderer>().color = Color.grey;
				break;
			case GrowingState.GrowingDying:
				GetComponent<SpriteRenderer>().color = Color.red;
				break;
			case GrowingState.Dead:
				GetComponent<SpriteRenderer>().color = Color.black;
				break;
		}
	}

	private void Kill() {
		this.ChangeState(GrowingState.Dead);
		this.Reset();
	}

	private void Reset() {
		this.waterLevel = 0.0f;
		this.growthLevel = 0.0f;
		this.growing = false;
	}
}
