using Godot;
using System;

public class UI : CanvasLayer
{
    [Signal]
    public delegate void TurnEndPressed();

    public Label TurnLabel;
    public Label ActionLabel;

    public override void _Ready()
    {
        TurnLabel = GetNode<Label>("TurnLabel");
        ActionLabel = GetNode<Label>("ActionLabel");
    }

    private void OnTurnEndPressed()
    {
        EmitSignal(nameof(TurnEndPressed));
    }
}
