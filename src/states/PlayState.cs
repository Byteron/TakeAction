using Godot;
using RelEcs;
using RelEcs.Godot;

public class PlayState : GameState
{
    public override void Init(GameStateController gameStates)
    {
        InitSystems
            .Add(new SpawnGameBoardSystem())
            .Add(new SpawnTokenSystem())
            .Add(new SpawnUISystem());
        
        InputSystems
            .Add(new SelectionSystem())
            .Add(new ActionSystem());
        
        UpdateSystems
            .Add(new MoveEventSystem())
            .Add(new DamageEventSystem())
            .Add(new TokenInfoSystem())
            .Add(new PlayerInfoSystem())
            .Add(new TurnEndSystem());
    }
}
