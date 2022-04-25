using Godot;
using RelEcs;
using RelEcs.Godot;

public struct MoveEvent
{
    public (int, int) Cell;
}

public class MoveEventSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((MoveEvent e) =>
        {
            GD.Print("Move Event Received!");
            var gameBoard = commands.GetResource<GameBoard>();
            var selectedToken = commands.GetResource<SelectedToken>();
            
            var token = selectedToken.Entity.Get<Node<Token>>().Value;
            ref var position = ref selectedToken.Entity.Get<Position>();
            
            var currentTile = gameBoard.Tiles[(position.X, position.Y)];
            var targetTile = gameBoard.Tiles[e.Cell];
            
            currentTile.Remove<HasToken>();
            targetTile.Add(new HasToken(selectedToken.Entity));

            position.X = e.Cell.Item1;
            position.Y = e.Cell.Item2;

            token.Position = position.FixedWorld;
            
            token.Scale = Vector2.One * 0.8f;
            selectedToken.Entity = default;
        });
    }
}