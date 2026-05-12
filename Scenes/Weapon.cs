using Godot;

public partial class Weapon : Node2D
{
	[Export] public PackedScene BulletScene;
	[Export] public float FireRate = 1.5f;

	private double fireTimer = 0;

	public override void _Process(double delta)
	{
		fireTimer -= delta;

		if (fireTimer <= 0)
		{
			Shoot();
			fireTimer = FireRate;
		}
	}

	private void Shoot()
	{
		if (BulletScene == null)
		{
			GD.Print("BulletScene missing!");
			return;
		}

		Bullet bullet = BulletScene.Instantiate<Bullet>();
		GetTree().CurrentScene.GetNode<Node2D>("Bullets").AddChild(bullet);

		bullet.GlobalPosition = GlobalPosition;

		Node2D nearestEnemy = GetNearestEnemy();

		if (nearestEnemy != null)
		{
			bullet.Direction = (nearestEnemy.GlobalPosition - GlobalPosition).Normalized();
		}
		else
		{
			bullet.Direction = Vector2.Right;
		}

		//GD.Print("Shoot!");
	}

	private Node2D GetNearestEnemy()
	{
		Node2D nearest = null;
		float nearestDistance = float.MaxValue;

		foreach (Node node in GetTree().GetNodesInGroup("enemy"))
		{
			if (node is not Node2D enemy)
				continue;

			float distance = GlobalPosition.DistanceTo(enemy.GlobalPosition);

			if (distance < nearestDistance)
			{
				nearestDistance = distance;
				nearest = enemy;
			}
		}

		return nearest;
	}
}
