using Godot;

namespace PlatformPunk.Slime;

public partial class Slime : Node2D
{
    private Vector2 _dir = Vector2.Right;

    public override void _Process(double delta)
    {
        base._Process(delta);
        var rayCastRight = GetNode<RayCast2D>("RayCastRight");
        var rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
        if (rayCastRight.IsColliding() && !rayCastLeft.IsColliding())
        {
            var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            animatedSprite.FlipH = true;
            _dir = Vector2.Left;
        }
        else if (rayCastLeft.IsColliding() && !rayCastRight.IsColliding())
        {
            var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
            animatedSprite.FlipH = false;
            _dir = Vector2.Right;
        }

        Position += (float)delta * Speed * _dir;
    }

    private const float Speed = 20.0f;
}