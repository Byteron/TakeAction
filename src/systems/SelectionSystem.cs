using Godot;
using RelEcs;
using RelEcs.Godot;

public class SelectionSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetResource<GameBoard>();
        var currentPlayer = commands.GetResource<CurrentPlayer>();
        var selectedToken = commands.GetResource<SelectedToken>();
        var sceneTree = commands.GetResource<SceneTree>();
        
        if (Input.IsActionJustPressed("select") && !selectedToken.Entity.IsAlive)
        {
            var mousePosition = sceneTree.Root.GetMousePosition();
            var tilePosition = mousePosition / (Vector2.One * 64f);
            var cell = ((int)tilePosition.x, (int)tilePosition.y);

            if (!gameBoard.Tiles.ContainsKey(cell))
            {
                return;
            }
            
            var tile = gameBoard.Tiles[cell];

            if (tile.Has<HasToken>())
            {
                GD.Print(cell, " selected");
                var tokenEntity = tile.Get<HasToken>().Entity;

                if (tokenEntity.Get<Team>().Value == currentPlayer.Value)
                {
                    selectedToken.Entity = tile.Get<HasToken>().Entity;
                    selectedToken.Entity.Get<Node<Sprite>>().Value.Scale = Vector2.One  * 0.9f;
                }
            }
        }
        
        if (Input.IsActionJustPressed("deselect") && selectedToken.Entity.IsAlive)
        {
            GD.Print("deselected");
            selectedToken.Entity.Get<Node<Sprite>>().Value.Scale = Vector2.One * 0.8f;
            selectedToken.Entity = default;
        }
    }
}