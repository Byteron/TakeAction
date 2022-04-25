using Godot;
using System;

public class Token : Node2D
{
    [Export] public int[] Costs = { 0 };
    
    public int Cost;
    public int NextCost;
    public int Index = -1;

    public override void _Ready()
    {
        Cycle();
    }

    public void Cycle()
    {
        Index = (Index + 1) % Costs.Length;
        var nextIndex = (Index + 1) % Costs.Length;
        Cost = Costs[Index];
        NextCost = Costs[nextIndex];
    }
}
