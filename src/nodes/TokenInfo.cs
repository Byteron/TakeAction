using Godot;

public class TokenInfo : Control
{
    public Label HealthLabel;
    public Label CostLabel;
    public Label NextCostLabel;

    public override void _Ready()
    {
        HealthLabel = GetNode<Label>("VBoxContainer/Health");
        CostLabel = GetNode<Label>("VBoxContainer/HBoxContainer/Cost");
        NextCostLabel = GetNode<Label>("VBoxContainer/HBoxContainer/NextCost");
    }
}
