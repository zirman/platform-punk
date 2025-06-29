using Godot;

namespace PlatformPunk.scripts;

public partial class Coin : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("I'm a coin!");
    }

    // Called every frame. 'delta' has been the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void _on_body_entered(Node2D body)
    {
        GD.Print("wat");
        QueueFree();
    }
}