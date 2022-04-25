using Godot;
using RelEcs;
using RelEcs.Godot;

// inherits Godot.Resource because it needs to be a Godot Object to be able to connect to godot signals
public class SpawnUISystem : Resource, ISystem
{
    private PackedScene uiScene = GD.Load<PackedScene>("res://src/nodes/UI.tscn");

    private Commands commands;
    
    public void Run(Commands commands)
    {
        this.commands = commands;

        var gameState = commands.GetResource<CurrentGameState>().State;
        
        var ui = uiScene.Instance<UI>();
        ui.Connect(nameof(UI.TurnEndPressed), this, nameof(OnTurnEndPressed));
        gameState.AddChild(ui);
        commands.AddResource(ui);
    }

    private void OnTurnEndPressed()
    {
        commands.Send<TurnEndEvent>();
    }
}