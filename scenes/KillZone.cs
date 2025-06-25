using Godot;
using System;

public partial class KillZone : Area2D
{
	public void _on_body_entered(Node2D body)
	{
		//$Timer
		GD.Print("You Died!");
	}
}
