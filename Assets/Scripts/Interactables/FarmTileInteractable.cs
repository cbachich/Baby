using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BabyGrowth))]
public class FarmTileInteractable : PlayerInteractable
{
	private BabyGrowth babyGrowth;

	private void Awake() {
		babyGrowth = GetComponent<BabyGrowth>();
	}

    public override void OnPlayerInteracting(PlayerInteract player)
    {
		if(player.HoldingState == PlayerHoldingState.Seed && babyGrowth.CurrentState == BabyGrowth.GrowingState.Dormant) {
			babyGrowth.PlantSeed();
			player.DropResource();
		}
		else if(player.HoldingState == PlayerHoldingState.Water && babyGrowth.IsGrowing()) {
			babyGrowth.FillWater();
			player.DropResource();
		}
    }
}
