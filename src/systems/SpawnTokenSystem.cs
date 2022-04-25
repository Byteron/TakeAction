using Godot;
using RelEcs;
using RelEcs.Godot;

public class SpawnTokenSystem : ISystem
{
    private static readonly (int, int)[] startPositions1 = new (int, int)[] { (1, 0), (2, 0), (3, 0) };
    private static readonly (int, int)[] startPositions2 = new (int, int)[] { (1, 4), (2, 4), (3, 4) };
    
    private PackedScene tokenScene = GD.Load<PackedScene>("res://src/nodes/Token.tscn");
    
    public void Run(Commands commands)
    {   
        var gameBoard = commands.GetResource<GameBoard>();
        var playerEntity1 = gameBoard.Players[0];
        var playerEntity2 = gameBoard.Players[1];
        var gameState = commands.GetResource<CurrentGameState>().State;

        foreach (var (x, y) in startPositions1)
        {
            var tileEntity = gameBoard.Tiles[(x, y)];
            var pos = new Position(new Vector2(x, y));
            
            var tokenNode = tokenScene.Instance<Token>();
            gameState.AddChild(tokenNode);

            tokenNode.Position = pos.Value * GameBoard.TileSize + GameBoard.TileSize / 2;
            
            var tokenEntity = commands.Spawn(tokenNode);
            tokenEntity.Add(pos).Add(new Health(10)).Add<BelongsTo>(playerEntity1);

            tileEntity.Add(new HasToken(tokenEntity));
        }
        
        foreach (var (x, y) in startPositions2)
        {
            var tileEntity = gameBoard.Tiles[(x, y)];
            var pos = new Position(new Vector2(x, y));
            
            var tokenNode = tokenScene.Instance<Token>();
            gameState.AddChild(tokenNode);

            tokenNode.Position = pos.Value * GameBoard.TileSize + GameBoard.TileSize / 2;;
            
            var tokenEntity = commands.Spawn(tokenNode);
            tokenEntity.Add(pos).Add(new Health(10)).Add<BelongsTo>(playerEntity2);

            tileEntity.Add(new HasToken(tokenEntity));
        }

        commands.AddResource(new SelectedToken());
    }
}