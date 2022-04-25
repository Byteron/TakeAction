using System.Runtime.CompilerServices;
using Godot;
using RelEcs;
using RelEcs.Godot;

public struct Team
{
    public int Value;
    public Team(int value) => Value = value;
}

public struct Health
{
    public int Value;
    public int Max;

    public Health(int value)
    {
        Max = value;
        Value = Max;
    }
    
    public void Hurt(int value)
    {
        Value = Value - value < 0 ? 0 : Value - value;
    }

    public bool IsAlive => Value > 0;
}

public struct Actions
{
    public int Value;
    public Actions(int value) => Value = value;
}

public struct Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public Position(Vector2 value)
    {
        X = (int)value.x;
        Y = (int)value.y;
    }
    
    public Vector2 Value => new Vector2(X, Y);
    public Vector2 World => new Vector2(X, Y) * GameBoard.TileSize;
    public Vector2 FixedWorld => new Vector2(X, Y) * GameBoard.TileSize + GameBoard.TileSize / 2;
    
    public bool IsNeighbor(Position other)
    {
        var diff_x = Mathf.Abs(X - other.X);
        var diff_y = Mathf.Abs(Y - other.Y);
        GD.Print(diff_x, " / ", diff_y);
        return  diff_x == 1 || diff_y == 1;
    }
}

public struct HasToken
{
    public Entity Entity;
    public HasToken(Entity entity) => Entity = entity;
}