using Godot;

namespace PlatformPunk.scripts;

public partial class KillZone : Area2D
{
    public void _on_body_entered(Node2D body)
    {
        GD.Print("You Died!");
        ((Timer)GetNode("Timer")).Start();
    }

    public void _on_timer_timeout()
    {
        GD.Print("Restart");
        GetTree().ReloadCurrentScene();
    }
}