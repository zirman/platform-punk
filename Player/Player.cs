using Godot;

namespace PlatformPunk.Player;

public partial class Player : CharacterBody2D
{
    private const float TopSpeed = 100f;
    private const float Acceleration = 200f;
    private const float JumpVelocity = 300f;
    private const float WallSlideFriction = 800f;
    private const float DoubleJumpRatio = .75f;
    private bool _doubleJump = false;

    public override void _PhysicsProcess(double delta)
    {
        var velocity = Velocity;
        var animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        // Add gravity acceleration
        if (!IsOnFloor()) velocity += GetGravity() * (float)delta;
        // Wall slide friction
        if (IsOnWall()) // TODO: make only work if walking into wall
        {
            velocity.Y = Mathf.MoveToward(velocity.Y, 0, (float)delta * WallSlideFriction);
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump"))
            if (IsOnFloor())
            {
                _doubleJump = true;
                velocity.Y = -JumpVelocity;
            }
            else if (IsOnWallOnly())
            {
                if (GetWallNormal().X > 0)
                {
                    velocity = Vector2.FromAngle(Mathf.Pi * 12.5f / 8) * JumpVelocity;
                }
                else
                {
                    velocity = Vector2.FromAngle(Mathf.Pi * 11.5f / 8) * JumpVelocity;
                }


            }
            else if (_doubleJump)
            {
                _doubleJump = false;
                velocity.Y = -JumpVelocity * DoubleJumpRatio;
            }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        var direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            // running or jumping animation
            animatedSprite.Play(IsOnFloor() ? "run" : "jump");
            animatedSprite.FlipH = direction.X < 0;
            velocity.X = velocity.X < 0 && direction.X > 0 || velocity.X > 0 && direction.X < 0
                ? Mathf.MoveToward(velocity.X, 0, (float)delta * Acceleration * 2)
                : float.Clamp(velocity.X + direction.X * (float)delta * Acceleration, -TopSpeed, TopSpeed);
        }
        else
        {
            // standing or jumping animation
            animatedSprite.Play(IsOnFloor() ? "idle" : "jump");
            velocity.X = Mathf.MoveToward(velocity.X, 0, (float)delta * Acceleration * 2);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
