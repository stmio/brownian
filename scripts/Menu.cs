using Godot;
using System;

public partial class Menu : Control {
	TextEdit num_particles;
	TextEdit temp_factor;
	TextEdit radius;
	CheckBox trace;
	Button simulate;
	
	Control options;
	RichTextLabel loading;
	
	Container container;
	
	public override void _Ready() {
		options = GetNode<Control>("Options");
		num_particles = GetNode<TextEdit>("Options/Particles/particle_no");
		temp_factor = GetNode<TextEdit>("Options/Temperature Factor/temp");
		radius = GetNode<TextEdit>("Options/Particle Radius/radius");
		trace = GetNode<CheckBox>("Options/Trace Particle/trace");
		simulate = GetNode<Button>("Options/simulate");
		loading = GetNode<RichTextLabel>("Loading");
		
		container = ResourceLoader
			.Load<PackedScene>("res://scenes/container.tscn")
			.Instantiate() as Container;
		
		simulate.Pressed += StartSimulation;
	}
	
	private async void StartSimulation() {
		options.Visible = false;
		loading.Visible = true;
		await ToSignal(GetTree().CreateTimer(0.2), "timeout");
		
		int n = Int32.Parse(num_particles.Text);
		float temp = float.Parse(temp_factor.Text);
		float rad = float.Parse(radius.Text);
		bool trail = trace.ButtonPressed; 
		
		container.Initalize(n, temp, rad, trail);
		GetTree().Root.AddChild(container);
	}
}
