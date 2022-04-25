using System;
using Godot;
using RelEcs;
using RelEcs.Godot;


public class PlayerInfoSystem : ISystem
{
    public void Run(Commands commands)
    {
        var gameBoard = commands.GetResource<GameBoard>();
        var playerEntity = gameBoard.GetCurrentPlayerEntity();
        var ui = commands.GetResource<UI>();
        
        if (!playerEntity.IsAlive || ui.ActionLabel == null)
        {
            return;
        }
        
        ui.ActionLabel.Text = $"Actions: {playerEntity.Get<Actions>().Value}";
        ui.TurnLabel.Text = $"Turn: {gameBoard.CurrentPlayer}";
    }
}