using Godot;
using System;

public class Token : Node2D
{
    private static readonly (int, int, int)[] sets =
    {
        (1, 3, -2), (2, -1, 1), (-3, 3, 2)
    };
    
    [Export] public int[] Costs = new int[3];
    
    public int Cost;
    public int NextCost;
    public int Index = -1;

    public override void _Ready()
    {
        var set = sets[GD.Randi() % 3];
        Costs[0] = set.Item1; 
        Costs[1] = set.Item2; 
        Costs[2] = set.Item3; 
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
