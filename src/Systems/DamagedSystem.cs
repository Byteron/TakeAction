using Godot;
using RelEcs;

public struct Damaged
{
    public Entity Entity;
    public int Amount;
}

public class DamagedSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((Damaged e) =>
        {
            GD.Print("Damage Received!");
            
            var gameBoard = commands.GetElement<GameBoard>();

            ref var health = ref e.Entity.Get<Health>();
            health.Hurt(e.Amount);

            if (health.IsAlive) return;

            GD.Print("Entity Died!");
            
            var position = e.Entity.Get<Position>();
            var tile = gameBoard.Tiles[(position.X, position.Y)];
            tile.Remove<HasToken>();
            
            e.Entity.Despawn();
        });
    }
}