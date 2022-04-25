using System.Collections.Generic;
using Godot;
using RelEcs;

public class GameBoard
{
    public static Vector2 TileSize = Vector2.One * 64f;
    
    public Dictionary<(int, int), Entity> Tiles = new Dictionary<(int, int), Entity>();
    public Dictionary<int, Entity> Players = new Dictionary<int, Entity>();
    public int CurrentPlayer = 0;

    public Entity GetCurrentPlayerEntity()
    {
        return Players[CurrentPlayer];
    }
}

public class SelectedToken
{
    public Entity Entity;
}