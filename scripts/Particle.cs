using Godot;
using System;

public partial class Particle : RigidBody2D {
	[Export] float tempFactor = 500;
	[Export] float radius = 5;
	[Export] bool enableTrail = false;
	
	Line2D trail;
	
	public override void _Ready() {
		if (!enableTrail) SetProcess(false);
		else {
			trail = GetNode<Line2D>("Trail");
			trail.TopLevel = true;
		}
		
		CollisionShape2D col = GetNode<CollisionShape2D>("Collider");
		CircleShape2D colShape = col.Shape as CircleShape2D;
		colShape.Radius = radius;
		
		SetRandomVelocity(tempFactor);
		SetRandomPos(new Vector2(0, 0), new Vector2(1152, 648));
	}
	
	public override void _Process(double delta) {
		trail.AddPoint(Position);
	}
	
	public override void _Draw() {
		DrawCirclePoly(new Vector2(0, 0), radius, new Color(1, 1, 1));
	}
	
	public double GetResultantVelocity() {
		return Math.Sqrt(
			Math.Pow(LinearVelocity.X, 2) + Math.Pow(LinearVelocity.Y, 2)
		);
	}
	
	public void SetRandomVelocity(float factor) {
		LinearVelocity = new Vector2(
			(float)GD.RandRange(-factor, factor),
			(float)GD.RandRange(-factor, factor)
		);
	}

	public void SetRandomPos(Vector2 topLeft, Vector2 bottomRight) {
		Position = new Vector2(
			(float)GD.RandRange(topLeft.X, bottomRight.X),
			(float)GD.RandRange(topLeft.Y, bottomRight.Y)
		);
	}
	
	private void DrawCirclePoly(Vector2 center, float radius, Color color) {
		int nbPoints = 16;
		var pointsArc = new Vector2[nbPoints + 2];
		pointsArc[0] = center;
		var colors = new Color[] { color };

		for (int i = 0; i <= nbPoints; i++) {
			float anglePoint = Mathf.DegToRad(i * 360 / nbPoints - 90);
			pointsArc[i + 1] = center + new Vector2(Mathf.Cos(anglePoint), Mathf.Sin(anglePoint)) * radius;
		}

		DrawPolygon(pointsArc, colors);
	}
}
