using Godot;
using System;
using RelEcs.Godot;

public class Main : Node2D
{
    [Export] Animation animation;
    
    public override void _Ready()
    {
        var gameStates = new GameStateController();
        AddChild(gameStates);
        gameStates.PushState(new PlayState());
    }
}
