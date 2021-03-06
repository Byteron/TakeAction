using Godot;
using RelEcs;
using RelEcs.Godot;

public struct TurnEnd { }

public class TurnEndSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((TurnEnd e) =>
        {
            GD.Print("Turn End Pressed");
            
            var gameBoard = commands.GetElement<GameBoard>();
            var selectedToken = commands.GetElement<SelectedToken>();

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