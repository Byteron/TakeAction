using Godot;
using RelEcs;
using RelEcs.Godot;

public struct TurnEndEvent { }

public class TurnEndSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((TurnEndEvent e) =>
        {
            GD.Print("Turn End Pressed");
            
            var currentPlayer = commands.GetResource<CurrentPlayer>();
            var selectedToken = commands.GetResource<SelectedToken>();
            var ui = commands.GetResource<UI>();
            
            if (selectedToken.Entity.IsAlive)
            {
                selectedToken.Entity.Get<Node<Sprite>>().Value.Scale = Vector2.One *0.8f;
                selectedToken.Entity = default;
            }
            
            currentPlayer.Value = (currentPlayer.Value + 1) % 2;
            ui.TurnLabel.Text = "" + currentPlayer.Value;
        });
    }
}