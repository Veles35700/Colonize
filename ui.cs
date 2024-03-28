using Godot;
using System;

public partial class ui : Control
{
	// Container holding the building buttons
	private Node buildingButtons;

	// Text displaying the food and metal resources
	private Label foodMetalText;

	// Text displaying the oxygen and energy resources
	private Label oxygenEnergyText;

	// Text showing our current turn
	private Label curTurnText;

	// Game manager object in order to access those functions and values
	private Node gameManager;

	public override void _Ready()
	{
		// Initialize components
		buildingButtons = GetNode("BuildingButtons");
		foodMetalText = GetNode<Label>("FoodMetalText");
		oxygenEnergyText = GetNode<Label>("OxygenEnergyText");
		curTurnText = GetNode<Label>("TurnText");
		gameManager = GetNode<Node>("/root/MainScene");

		// Update the UI when the game starts
		UpdateResourceText();
		OnEndTurn();
	}

	// Called when a turn is over - resets the UI
	public void OnEndTurn()
	{
		// Update the current turn text and enable the building buttons
		curTurnText.Text = "Turn: " + gameManager.Get("curTurn");
		//buildingButtons.Visible = true;
	}

	// Updates the resource text to show the current values
	public void UpdateResourceText()
	{
		// Set the food and metal text
		string foodMetal = gameManager.Get("curFood") + " (" + ((int)gameManager.Get("foodPerTurn") >= 0 ? "+" : "") + gameManager.Get("foodPerTurn") + ")\n"
			+ gameManager.Get("curMetal") + " (" + ((int)gameManager.Get("metalPerTurn") >= 0 ? "+" : "") + gameManager.Get("metalPerTurn") + ")";
		foodMetalText.Text = foodMetal;

		// Set the oxygen and energy text
		string oxygenEnergy = gameManager.Get("curOxygen") + " (" + ((int)gameManager.Get("oxygenPerTurn") >= 0 ? "+" : "") + gameManager.Get("oxygenPerTurn") + ")\n"
			+ gameManager.Get("curEnergy") + " (" + ((int)gameManager.Get("energyPerTurn") >= 0 ? "+" : "") + gameManager.Get("energyPerTurn") + ")";
		oxygenEnergyText.Text = oxygenEnergy;
	}
	
	private void _on_mine_button_pressed()
	{
		//buildingButtons.Visible = false;
		gameManager.Call("OnSelectBuilding", 1);
	}

	private void _on_greenhouse_button_pressed()
	{
		//buildingButtons.Visible = false;
		gameManager.Call("OnSelectBuilding", 2);
	}

	// Called when the Solar Panel building button is pressed
	private void _on_solar_panel_button_pressed()
	{
		//buildingButtons.Visible = false;
		gameManager.Call("OnSelectBuilding", 3);
	}

	// Called when the "End Turn" button is pressed
	private void _on_end_turn_button_pressed()
	{
		gameManager.Call("EndTurn");
	}
}
