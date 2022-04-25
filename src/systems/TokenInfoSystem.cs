using Godot;
using RelEcs;
using RelEcs.Godot;


public class TokenInfoSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.ForEach((ref Node<Token> token, ref Health health, ref Node<TokenInfo> info, ref Node<Sprite> sprite, ref Team team) =>
        {
            info.Value.HealthLabel.Text = "" + health.Value;
            info.Value.CostLabel.Text = "" + -token.Value.Cost;
            info.Value.NextCostLabel.Text = "" + -token.Value.NextCost;

            if (team.Value == 0)
            {
                sprite.Value.Modulate = Colors.LightCyan;
            }
            else
            {
                sprite.Value.Modulate = Colors.LightGoldenrod;
            }
        });
    }
}