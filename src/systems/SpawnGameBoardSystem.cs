using Godot;
using RelEcs;
using RelEcs.Godot;

public class SpawnGameBoardSystem : ISystem
{
    private PackedScene tileScene = GD.Load<PackedScene>("res://src/nodes/Tile.tscn");

    public void Run(Commands commands)
    {
        GD.Randomize();
        
        var gameBoard = new GameBoard();
        var gameState = commands.GetResource<CurrentGameState>().State;

        var player1 = commands.Spawn().Add(new Actions(5)).Add(new Team(0));
        var player2 = commands.Spawn().Add(new Actions(5)).Add(new Team(1));
        
        gameBoard.Players.Add(0, player1);
        gameBoard.Players.Add(1, player2);
        
        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                var position = new Vector2(x, y);
                
                var tileNode = tileScene.Instance<Tile>();
                gameState.AddChild(tileNode);
                
                var tileEntity = commands.Spawn(tileNode);
                tileEntity.Add(new Position(position));

                tileNode.Position = position * GameBoard.TileSize;
                
                gameBoard.Tiles.Add((x, y), tileEntity);
            }
        }
        
        commands.AddResource(gameBoard);

        commands.ForEach((ref Node<Tile> tile, ref Node<Label> label) =>
        {
            tile.Value.Cost = (int)(GD.RandRange(-1, 1) + GD.RandRange(-1, 1) + GD.RandRange(-1, 1));
            label.Value.Text = "" + -tile.Value.Cost;
        });

        commands.Send<TurnEndEvent>();
    }
}