using Godot;
using System;

public partial class BuildingData : Node
{
	static public Building baseBuilding;
	static public Building mine;
	static public Building greenhouse;
	static public Building solarpanel;
	
	public BuildingData() {
		baseBuilding = new Building(0, (Texture2D)GD.Load("res://Sprites/Base.png"), 0, 0, 0, 0);
		mine = new Building(1, (Texture2D)GD.Load("res://Sprites/Mine.png"), 2, 1, 4, 1);
		greenhouse = new Building(2, (Texture2D)GD.Load("res://Sprites/Greenhouse.png"), 1, 1, 0, 0);
		solarpanel = new Building(3, (Texture2D)GD.Load("res://Sprites/SolarPanel.png"), 4, 1, 0, 0);
	}
	
	public Texture2D getTexture(int type) {
		if (type == 0) {
			return baseBuilding.iconTexture;
		} else if (type == 1) {
			return mine.iconTexture;
		} else if (type == 2) {
			return greenhouse.iconTexture;
		} else {
			return solarpanel.iconTexture;
		}
	}
}

public partial class Building : Node
{
	// Building type
	public int type;
	
	// Building texture
	public Texture2D iconTexture;
	
	// Resource the building produces
	public int prodResource = 0;
	public int prodResourceAmount;
	
	// Resource the building needs to be maintained
	public int upkeepResource = 0;
	public int upkeepResourceAmount;
	
	// Constructor
	public Building(int type, Texture2D iconTexture, int prodResource, int prodResourceAmount, int upkeepResource, int upkeepResourceAmount)
	{
		this.type = type;
		this.iconTexture = iconTexture;
		this.prodResource = prodResource;
		this.prodResourceAmount = prodResourceAmount;
		this.upkeepResource = upkeepResource;
		this.upkeepResourceAmount = upkeepResourceAmount;
	}
}
