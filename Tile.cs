using Godot;
using System;

public partial class Tile : Area2D
{
	[Export] 
	public bool startTile = false;
	
	public bool hasBuilding = false;
	public bool canPlaceBuilding = false;
	
	private Sprite2D highlight;
	private Sprite2D buildingIcon;
	
	public override void _Ready()
	{
		AddToGroup("Tiles");
		
		highlight = GetNode<Sprite2D>("Highlight");
		buildingIcon = GetNode<Sprite2D>("BuildingIcon");
	}
	
	public void ToggleHighlight(bool toggle) 
	{
		highlight.Visible = toggle;
		canPlaceBuilding = toggle;
	}
	
	public void placeBuilding(Texture2D buildingTexture)
	{
		hasBuilding = true;
		buildingIcon.Texture = buildingTexture;
	}
	
	private void _on_input_event(Node viewport, InputEvent @event, long shape_idx)
	{
	if (@event is InputEventMouseButton && ((InputEventMouseButton)@event).Pressed)
		{
			GameManager gameManager = GetNode<GameManager>("/root/MainScene");
			
			if(gameManager.currentlyPlacingBuilding && canPlaceBuilding)
			{
				gameManager.PlaceBuilding(this);
			}
		}
	}
}
