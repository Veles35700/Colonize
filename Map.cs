using Godot;
using System;

public partial class Map : Node
{
	private Godot.Collections.Array<Godot.Node> allTiles;
	private Godot.Collections.Array<Godot.Node> tilesWithBuildings;
	private float tileSize = 64.0f;

	// Called every time the node is added to the scene
	public override void _Ready()
	{
		allTiles = GetTree().GetNodesInGroup("Tiles");
		tilesWithBuildings = new Godot.Collections.Array<Godot.Node>{};

		// Find the start tile and place the Base building
		foreach (Tile tile in allTiles)
		{
			//Tile tile = tileNode as Tile;
			if (tile != null && tile.startTile)
			{
				//tilesWithBuildings.Add(tile);
				//tile.placeBuilding(BuildingData.baseBuilding.iconTexture);
				//DisableTileHighlights();
				PlaceBuilding(tile, BuildingData.baseBuilding.iconTexture);
				break;
			}
		}
	}

	// Returns a tile at the given position - returns null if no tile is found
	private Tile GetTileAtPosition(Vector2 position)
	{
		foreach (Tile tile in allTiles)
		{
			//Tile tile = tileNode as Tile;
			if (tile != null && tile.Position == position && !tile.hasBuilding)
			{
				return tile;
			}
		}
		return null;
	}

	// Highlights the tiles we can place buildings on
	private void HighlightAvailableTiles()
	{
		foreach (Tile tile in tilesWithBuildings)
		{
			Vector2 tilePosition = tile.Position;

			Tile northTile = GetTileAtPosition(tilePosition + new Vector2(0, tileSize));
			Tile southTile = GetTileAtPosition(tilePosition + new Vector2(0, -tileSize));
			Tile eastTile = GetTileAtPosition(tilePosition + new Vector2(tileSize, 0));
			Tile westTile = GetTileAtPosition(tilePosition + new Vector2(-tileSize, 0));

			if (northTile != null)
				northTile.ToggleHighlight(true);
			if (southTile != null)
				southTile.ToggleHighlight(true);
			if (eastTile != null)
				eastTile.ToggleHighlight(true);
			if (westTile != null)
				westTile.ToggleHighlight(true);
		}
	}

	// Disables all of the tile highlights
	private void DisableTileHighlights()
	{
		foreach (Tile tile in allTiles)
		{
			tile.ToggleHighlight(false);
		}
	}

	// Places down a building on the map
	private void PlaceBuilding(Tile tile, Texture2D texture)
	{
		tilesWithBuildings.Add(tile);
		tile.placeBuilding(texture);
		DisableTileHighlights();
	}
}
