using Godot;
using RelEcs;

public struct Team
{
    public int Value;
    public Team(int value) => Value = value;
}

public struct Actions
{
    public int Value;
    public Actions(int value) => Value = value;
}

public struct Position
{
    public Vector2 Value;
    public Position(Vector2 value) => Value = value;
}

public struct HasToken
{
    public Entity Entity;
    public HasToken(Entity entity) => Entity = entity;
}