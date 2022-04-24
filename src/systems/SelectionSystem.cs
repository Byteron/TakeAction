using Godot;
using RelEcs;
using RelEcs.Godot;

public class SelectionSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetResource<GameBoard>();
        var selectedUnit = commands.GetResource<SelectedToken>();
        var sceneTree = commands.GetResource<SceneTree>();
        
        if (Input.IsActionJustPressed("select") && !selectedUnit.Entity.IsAlive)
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
                selectedUnit.Entity = tile.Get<HasToken>().Entity;
                selectedUnit.Entity.Get<Node<Sprite>>().Value.Scale = Vector2.One  * 0.9f;
            }
        }
        
        if (Input.IsActionJustPressed("deselect") && selectedUnit.Entity.IsAlive)
        {
            GD.Print("deselected");
            selectedUnit.Entity.Get<Node<Sprite>>().Value.Scale = Vector2.One * 0.8f;
            selectedUnit.Entity = default;
        }
    }
}