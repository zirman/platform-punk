using Godot;

namespace PlatformPunk.scripts;

public partial class KillZone : Area2D
{
    public void _on_body_entered(Node2D body)
    {
        GD.Print("You Died!");
        Engine.TimeScale = .5;
        body.GetNode<CollisionShape2D>("CollisionShape2D").QueueFree();
        GetNode<Timer>("Timer").Start();
    }

    public void _on_timer_timeout()
    {
        GD.Print("Restart");
        Engine.TimeScale = 1;
        GetTree().ReloadCurrentScene();
    }
}
