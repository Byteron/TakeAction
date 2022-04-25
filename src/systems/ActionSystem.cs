using Godot;
using RelEcs;
using RelEcs.Godot;

public class ActionSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetResource<GameBoard>();
        var playerEntity = gameBoard.GetCurrentPlayerEntity();
        var selectedToken = commands.GetResource<SelectedToken>();
        var sceneTree = commands.GetResource<SceneTree>();
        
        if (Input.IsActionJustPressed("select") && selectedToken.Entity.IsAlive)
        {
            GD.Print("Try Action");
            var mousePosition = sceneTree.Root.GetMousePosition();
            var targetPosition = new Position(mousePosition / GameBoard.TileSize);;
            var targetCell = (targetPosition.X, targetPosition.Y);
            
            if (!gameBoard.Tiles.TryGetValue(targetCell, out var targetTile))
            {
                GD.Print("Tile Not Found");
                return;
            }
            
            var currentPosition = selectedToken.Entity.Get<Position>();

            if (!targetPosition.IsNeighbor(currentPosition)) return;

            if (!targetTile.Has<HasToken>())
            {
                GD.Print("Move Event Spawned!");
                commands.Send(new MoveEvent() { Cell = targetCell });
            }
            else
            {
                var targetToken = targetTile.Get<HasToken>().Entity;

                if (targetToken.Get<Team>().Value == selectedToken.Entity.Get<Team>().Value) return;
                
                GD.Print("Damage Event Spawned!");
                commands.Send(new DamageEvent() { Entity = targetTile.Get<HasToken>().Entity, Amount = 1 });
            }
        }
    }
}