using Godot;
using System;

public partial class GameManager : Node2D
{
	// Current amount of each resource we have
	public int curFood = 0;
	public int curMetal = 0;
	public int curOxygen = 0;
	public int curEnergy = 0;

	// Amount of each resource we get each turn
	public int foodPerTurn = 0;
	public int metalPerTurn = 0;
	public int oxygenPerTurn = 0;
	public int energyPerTurn = 0;

	public int curTurn = 1;

	// Are we currently placing down a building?
	public bool currentlyPlacingBuilding = false;

	// Type of building we're currently placing
	public int buildingToPlace;

	// Components
	private Node ui;
	private Node map;

	public override void _Ready()
	{
		// Initialize components
		ui = GetNode<Node>("UI");
		map = GetNode<Node>("Tiles");

		// Update the UI when the game starts
		ui.CallDeferred("UpdateResourceText");
		ui.CallDeferred("OnEndTurn");
	}

	// Called when the player ends the turn
	public void EndTurn()
	{
		// Update our current resource amounts
		curFood += foodPerTurn;
		curMetal += metalPerTurn;
		curOxygen += oxygenPerTurn;
		curEnergy += energyPerTurn;

		// Increase current turn
		curTurn += 1;

		// Update the UI
		ui.CallDeferred("UpdateResourceText");
		ui.CallDeferred("OnEndTurn");
	}

	// Called when we've selected a building to place
	public void OnSelectBuilding(int buildingType)
	{
		currentlyPlacingBuilding = true;
		buildingToPlace = buildingType;

		// Highlight the tiles we can place a building on
		map.CallDeferred("HighlightAvailableTiles");
	}

	// Called when we place a building down on the grid
	public void PlaceBuilding(Node tileToPlaceOn)
	{
		currentlyPlacingBuilding = false;
		Texture texture = null;

		// Are we placing down a Mine?
		if (buildingToPlace == 1)
		{
			texture = BuildingData.mine.iconTexture;

			AddToResourcePerTurn(BuildingData.mine.prodResource, BuildingData.mine.prodResourceAmount);
			AddToResourcePerTurn(BuildingData.mine.upkeepResource, -BuildingData.mine.upkeepResourceAmount);
		}
		// Are we placing down a Greenhouse?
		else if (buildingToPlace == 2)
		{
			texture = BuildingData.greenhouse.iconTexture;

			AddToResourcePerTurn(BuildingData.greenhouse.prodResource, BuildingData.greenhouse.prodResourceAmount);
			AddToResourcePerTurn(BuildingData.greenhouse.upkeepResource, -BuildingData.greenhouse.upkeepResourceAmount);
		}
		// Are we placing down a Solar Panel?
		else if (buildingToPlace == 3)
		{
			texture = BuildingData.solarpanel.iconTexture;

			AddToResourcePerTurn(BuildingData.solarpanel.prodResource, BuildingData.solarpanel.prodResourceAmount);
			AddToResourcePerTurn(BuildingData.solarpanel.upkeepResource, -BuildingData.solarpanel.upkeepResourceAmount);
		}

		// Place the building on the map
		map.CallDeferred("PlaceBuilding", tileToPlaceOn, texture);

		// Update the UI to show changes immediately
		ui.CallDeferred("UpdateResourceText");
	}

	// Adds an amount to a certain resource per turn
	public void AddToResourcePerTurn(int resource, int amount)
	{
		// Resource 0 means none, so return
		if (resource == 0)
			return;
		else if (resource == 1)
			foodPerTurn += amount;
		else if (resource == 2)
			metalPerTurn += amount;
		else if (resource == 3)
			oxygenPerTurn += amount;
		else if (resource == 4)
			energyPerTurn += amount;
	}
}
