using Godot;
using RelEcs;
using RelEcs.Godot;

public class PlayState : GameState
{
    public override void Init(GameStateController gameStates)
    {
        InputSystems.Add(new SelectionSystem());
        InitSystems.Add(new SpawnGameBoardSystem());
        InitSystems.Add(new SpawnTokenSystem());
        UpdateSystems.Add(new TurnEndSystem());
    }
}
