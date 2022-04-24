using System.Collections.Generic;
using RelEcs;

public class GameBoard
{
    public Dictionary<(int, int), Entity> Tiles = new Dictionary<(int, int), Entity>();
}

public class SelectedToken
{
    public Entity Entity;
}

public class CurrentPlayer
{
    public int Value;
    public CurrentPlayer(int value) => Value = value;
}