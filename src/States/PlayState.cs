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
            .Add(new MoveSystem())
            .Add(new DamageSystem())
            .Add(new TokenInfoSystem())
            .Add(new PlayerInfoSystem())
            .Add(new TurnEndSystem());
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        GD.Print(@event);
    }

    int count = 0;

    public override void _Process(float delta)
    {
        count += 1;
        GD.Print(count);
    }
}
