using Godot;
using RelEcs;
using RelEcs.Godot;

public class SpawnGameBoardSystem : Resource, ISystem
{
    private static readonly Vector2 TileSize = new Vector2(64f, 64f);

    private PackedScene tileScene = GD.Load<PackedScene>("res://src/nodes/Tile.tscn");
    private PackedScene uiScene = GD.Load<PackedScene>("res://src/nodes/UI.tscn");

    private Commands commands;
    
    public void Run(Commands commands)
    {
        this.commands = commands;
        
        GD.Randomize();
        
        var gameBoard = new GameBoard();
        var gameState = commands.GetResource<CurrentGameState>().State;

        commands.Spawn().Add(new Actions(5)).Add(new Team(0));
        commands.Spawn().Add(new Actions(5)).Add(new Team(1));

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                var position = new Vector2(x, y);
                
                var tileNode = tileScene.Instance<Tile>();
                gameState.AddChild(tileNode);
                
                var tileEntity = commands.Spawn(tileNode);
                tileEntity.Add(new Position(position));

                tileNode.Position = position * TileSize;
                
                gameBoard.Tiles.Add((x, y), tileEntity);
            }
        }
        
        commands.AddResource(gameBoard);

        commands.ForEach((ref Node<Tile> tile, ref Node<Label> label) =>
        {
            tile.Value.Cost = (int)(GD.RandRange(-1, 1) + GD.RandRange(-1, 1) + GD.RandRange(-1, 1));
            label.Value.Text = "" + tile.Value.Cost;
        });
        
        commands.AddResource(new CurrentPlayer(-1));
        
        var ui = uiScene.Instance<UI>();
        ui.Connect(nameof(UI.TurnEndPressed), this, nameof(OnTurnEndPressed));
        gameState.AddChild(ui);
        commands.AddResource(ui);
        
        commands.Send<TurnEndEvent>();
    }

    private void OnTurnEndPressed()
    {
        commands.Send<TurnEndEvent>();
    }
}