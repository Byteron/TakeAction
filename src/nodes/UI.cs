using Godot;
using System;

public class UI : CanvasLayer
{
    [Signal]
    public delegate void TurnEndPressed();

    public Label TurnLabel;

    public override void _Ready()
    {
        TurnLabel = GetNode<Label>("TurnLabel");
    }

    private void OnTurnEndPressed()
    {
        EmitSignal(nameof(TurnEndPressed));
    }
}
