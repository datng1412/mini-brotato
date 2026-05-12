using Godot;

public partial class Enemy : CharacterBody2D
{
	[Export]
	public float Speed = 120f;

	private Player player;

	public override void _Ready()
	{
		player = GetTree()
			.GetFirstNodeInGroup("player") as Player;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (player == null)
			return;

		Vector2 direction =
			(player.GlobalPosition - GlobalPosition)
			.Normalized();

		Velocity = direction * Speed;

		MoveAndSlide();
	}
}
