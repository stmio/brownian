using Godot;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

public partial class Container : Node2D {
	[Export] string exportPath = Directory.GetCurrentDirectory() + "/brownian.txt";
	[Export] int numParticles = 1000;
	List<Particle> particles = new List<Particle>();
	
	// Params to assign to instantiated particles
	private float tempFactor;
	private float radius;
	
	public void Initalize(int n, float temp, float rad, bool trail) {
		Particle largeParticle = GetNode<RigidBody2D>("LargeParticle") as Particle;
		largeParticle.Set("tempFactor", temp);
		largeParticle.Set("enableTrail", trail);
		
		numParticles = n;
		tempFactor = temp;
		radius = rad;
	}
	
	public override void _Ready() {
		PackedScene particleScene = GD.Load<PackedScene>("res://scenes/particle.tscn");
		GD.Randomize();
		
		for (int i = 0; i < numParticles; i++) {
			Particle particle = particleScene.Instantiate() as Particle;
			particle.Set("tempFactor", tempFactor);
			particle.Set("radius", radius);
		
			AddChild(particle);
			particles.Add(particle);
		}
	}
	
	public override void _Notification(int what) {
		if (what == NotificationWMCloseRequest) {
			FileStream fi = new FileStream(exportPath, FileMode.Create);
			
			foreach (Particle p in particles) {
				double vel = p.GetResultantVelocity();
				string text = vel.ToString() + "\n";
				fi.Write(Encoding.UTF8.GetBytes(text), 0, text.Length);
			}
			
			fi.Close();
			GetTree().Quit();
		}
	}
}
