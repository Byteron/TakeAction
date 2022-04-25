using Godot;
using RelEcs;
using RelEcs.Godot;


public class TokenInfoSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetResource<GameBoard>();

        if (gameBoard.Players.Count == 0) return;

        var playerEntity1 = gameBoard.Players[0];
        var playerEntity2 = gameBoard.Players[1];

        commands.ForEach((ref Node<Token> token, ref Health health, ref Node<TokenInfo> info) =>
        {
            info.Value.HealthLabel.Text = "" + health.Value;
            info.Value.CostLabel.Text = "" + -token.Value.Cost;
            info.Value.NextCostLabel.Text = "" + -token.Value.NextCost;
        });
        
        var player1Tokens = commands.Query().Has<BelongsTo>(playerEntity1).Has<Node<Token>, Node<Sprite>, Health>();
        var player2Tokens = commands.Query().Has<BelongsTo>(playerEntity2).Has<Node<Token>, Node<Sprite>, Health>();
        
        player1Tokens.ForEach((ref Node<Sprite> sprite) =>
        {
            sprite.Value.Modulate = Colors.LightCyan;
        });
        
        player2Tokens.ForEach((ref Node<Sprite> sprite) =>
        {
            sprite.Value.Modulate = Colors.LightGoldenrod;
        });
    }
}