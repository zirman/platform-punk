using System;
using Godot;

namespace PlatformPunk.Player;

public partial class Player : CharacterBody2D
{
    private const float TopSpeed = 100.0f;
    private const float Acceleration = 130.0f;
    private const float JumpVelocity = -300.0f;

    private bool _doubleJump = false;

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor()) velocity += GetGravity() * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump"))
        {
            if (IsOnFloor())
            {
                _doubleJump = true;
                velocity.Y = JumpVelocity;
            }
            else if (_doubleJump)
            {
                _doubleJump = false;
                velocity.Y = JumpVelocity * .75f;
            }
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        var direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");
        var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (direction != Vector2.Zero)
        {
            animatedSprite.Play(IsOnFloor() ? "run" : "jump");
            animatedSprite.FlipH = direction.X < 0;
            if (velocity.X < 0 && direction.X > 0 || velocity.X > 0 && direction.X < 0)
                velocity.X = Mathf.MoveToward(velocity.X, 0, (float)delta * Acceleration * 2);
            else
                velocity.X = float.Clamp(velocity.X + direction.X * (float)delta * Acceleration, -TopSpeed, TopSpeed);
        }
        else
        {
            animatedSprite.Play(IsOnFloor() ? "idle" : "jump");
            velocity.X = Mathf.MoveToward(velocity.X, 0, (float)delta * Acceleration * 2);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
