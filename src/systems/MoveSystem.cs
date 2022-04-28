using System;
using Godot;
using RelEcs;
using RelEcs.Godot;

public struct Move
{
    public Entity Entity;
    public (int, int) Cell;
}

public class MoveSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((Move e) =>
        {
            GD.Print("Move Received!");
            var gameBoard = commands.GetElement<GameBoard>();
            
            var token = e.Entity.Get<Node<Token>>().Value;
            ref var position = ref e.Entity.Get<Position>();
            
            var currentTile = gameBoard.Tiles[(position.X, position.Y)];
            var targetTile = gameBoard.Tiles[e.Cell];

            currentTile.Remove<HasToken>();
            targetTile.Add(new HasToken(e.Entity));

            position.X = e.Cell.Item1;
            position.Y = e.Cell.Item2;

            token.Position = position.FixedWorld;
        });
    }
}