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
            
            var gameBoard = commands.GetResource<GameBoard>();
            var selectedToken = commands.GetResource<SelectedToken>();

            if (selectedToken.Entity.IsAlive)
            {
                selectedToken.Entity.Get<Node<Token>>().Value.Scale = Vector2.One *0.9f;
                selectedToken.Entity = default;
            }
            
            gameBoard.CurrentPlayer = (gameBoard.CurrentPlayer + 1) % 2;

            var playerEntity = gameBoard.GetCurrentPlayerEntity();
            playerEntity.Get<Actions>().Value = 5;
        });
    }
}