using Godot;
using RelEcs;
using RelEcs.Godot;

public class SpawnTokenSystem : Resource, ISystem
{
    private static readonly Vector2 TileSize = new Vector2(64f, 64f);
    
    private static readonly (int, int)[] startPositions1 = new (int, int)[] { (1, 0), (2, 0), (3, 0) };
    private static readonly (int, int)[] startPositions2 = new (int, int)[] { (1, 4), (2, 4), (3, 4) };
    
    private PackedScene tokenScene = GD.Load<PackedScene>("res://src/nodes/Token.tscn");

    
    public void Run(Commands commands)
    {   
        var gameBoard = commands.GetResource<GameBoard>();
        var gameState = commands.GetResource<CurrentGameState>().State;

        foreach (var (x, y) in startPositions1)
        {
            var tileEntity = gameBoard.Tiles[(x, y)];
            var pos = new Position(new Vector2(x, y));
            
            var tokenNode = tokenScene.Instance<Token>();
            gameState.AddChild(tokenNode);

            tokenNode.Position = pos.Value * TileSize + TileSize / 2;
            
            var rabbitEntity = commands.Spawn(tokenNode);
            rabbitEntity.Add(pos).Add(new Team(0)).Add(new Health(10));

            tileEntity.Add(new HasToken(rabbitEntity));
        }
        
        foreach (var (x, y) in startPositions2)
        {
            var tileEntity = gameBoard.Tiles[(x, y)];
            var pos = new Position(new Vector2(x, y));
            
            var tokenNode = tokenScene.Instance<Token>();
            gameState.AddChild(tokenNode);

            tokenNode.Position = pos.Value * TileSize + TileSize / 2;
            
            var rabbitEntity = commands.Spawn(tokenNode);
            rabbitEntity.Add(pos).Add(new Team(1)).Add(new Health(10));

            tileEntity.Add(new HasToken(rabbitEntity));
        }

        commands.AddResource(new SelectedToken());
    }
}