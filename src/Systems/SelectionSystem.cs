using Godot;
using RelEcs;
using RelEcs.Godot;

public class SelectionSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetElement<GameBoard>();
        var selectedToken = commands.GetElement<SelectedToken>();
        var sceneTree = commands.GetElement<SceneTree>();
        var currentPlayer = gameBoard.GetCurrentPlayerEntity();
        
        if (Input.IsActionJustPressed("select") && !selectedToken.Entity.IsAlive)
        {
            var mousePosition = sceneTree.Root.GetMousePosition();
            var tilePosition = mousePosition / GameBoard.TileSize;
            var cell = ((int)tilePosition.x, (int)tilePosition.y);

            if (!gameBoard.Tiles.ContainsKey(cell)) return;

            var tile = gameBoard.Tiles[cell];

            if (tile.Has<HasToken>())
            {
                GD.Print(cell, " selected");
                var tokenEntity = tile.Get<HasToken>().Entity;

                if (tokenEntity.Has<BelongsTo>(currentPlayer))
                {
                    selectedToken.Entity = tile.Get<HasToken>().Entity;
                    selectedToken.Entity.Get<Node<Token>>().Value.Scale = Vector2.One;
                }
            }
        }
        
        if (Input.IsActionJustPressed("deselect") && selectedToken.Entity.IsAlive)
        {
            GD.Print("deselected");
            selectedToken.Entity.Get<Node<Token>>().Value.Scale = Vector2.One * 0.9f;
            selectedToken.Entity = default;
        }
    }
}