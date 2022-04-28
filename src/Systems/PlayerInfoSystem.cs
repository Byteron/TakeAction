using RelEcs;

public class PlayerInfoSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetElement<GameBoard>();
        var playerEntity = gameBoard.GetCurrentPlayerEntity();
        var ui = commands.GetElement<UI>();
        
        if (!playerEntity.IsAlive || ui.ActionLabel == null) return;

        ui.ActionLabel.Text = $"Actions: {playerEntity.Get<Actions>().Value}";
        ui.TurnLabel.Text = $"Turn: {gameBoard.CurrentPlayer}";
    }
}