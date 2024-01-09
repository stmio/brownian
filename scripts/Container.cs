using Godot;
using System;
using System.IO;
using System.Text;

public partial class Container : Node2D {
	[Export] string exportPath = Directory.GetCurrentDirectory() + "/brownian.txt";
	
	static int numParticles = 1000;
	Particle[] particles = new Particle[numParticles];
	
	public override void _Ready() {
		GD.Randomize();
		PackedScene particleScene = GD.Load<PackedScene>("res://scenes/particle.tscn");
		
		for (int i = 0; i < numParticles; i++) {
			Particle particle = particleScene.Instantiate() as Particle;
			AddChild(particle);
			particles[i] = particle;
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
