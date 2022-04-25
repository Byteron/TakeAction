using Godot;
using RelEcs;
using RelEcs.Godot;

public class ActionSystem : ISystem
{
    public void Run(Commands commands)
    {
        var selectedToken = commands.GetResource<SelectedToken>();

        if (!Input.IsActionJustPressed("select") || !selectedToken.Entity.IsAlive) return;
        
        GD.Print("Try Action");
            
        var gameBoard = commands.GetResource<GameBoard>();
        var playerEntity = gameBoard.GetCurrentPlayerEntity();
        var sceneTree = commands.GetResource<SceneTree>();
            
        ref var actions = ref playerEntity.Get<Actions>();
        var token = selectedToken.Entity.Get<Node<Token>>().Value;

        var mousePosition = sceneTree.Root.GetMousePosition();
        var targetPosition = new Position(mousePosition / GameBoard.TileSize);;
        var targetCell = (targetPosition.X, targetPosition.Y);
            
        if (!gameBoard.Tiles.TryGetValue(targetCell, out var targetTileEntity))
        {
            GD.Print("Tile Not Found");
            return;
        }

        var currentPosition = selectedToken.Entity.Get<Position>();

        if (!targetPosition.IsNeighbor(currentPosition)) return;

        if (!targetTileEntity.Has<HasToken>())
        {
            var targetTile = targetTileEntity.Get<Node<Tile>>().Value;

            if (actions.Value < token.Cost + targetTile.Cost) return;

            GD.Print("Move Event Spawned!");
            commands.Send(new MoveEvent() { Entity = selectedToken.Entity, Cell = targetCell });
            actions.Value -= token.Cost + targetTile.Cost;
            token.Cycle();
            token.Scale = Vector2.One * 0.9f;
            selectedToken.Entity = default;
        }
        else
        {
            if (actions.Value < token.Cost)
            {
                return;
            }
            
            GD.Print("Damage Event Spawned!");
                
            var targetToken = targetTileEntity.Get<HasToken>().Entity;

            if (targetToken.Has<BelongsTo>(playerEntity)) return;

            commands.Send(new DamageEvent() { Entity = targetTileEntity.Get<HasToken>().Entity, Amount = 1 });
            actions.Value -= token.Cost;
            token.Cycle();
            token.Scale = Vector2.One * 0.9f;
            selectedToken.Entity = default;
        }
    }
}