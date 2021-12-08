using System;
using Godot;

public class AddNote : Button
{
	public override void _Ready()
	{
		// Connect signals to functions
		Connect("button_down", this, "_OnButtonDown");
	}

	// Signal
	public void _OnButtonDown()
	{
		NewNote();
	}

	public void NewNote()
	{
		Control scene = ResourceLoader.Load<PackedScene>("res://premade_assets/note.tscn", noCache: true).Instance<Control>();
		Control persistentNodes = GetNode<Control>("/root/Node/Control/Persistent nodes");

		persistentNodes.AddChild(scene);
		scene.Owner = persistentNodes;

		// ColorRect note = (ColorRect)scene;
		// scene.RectGlobalPosition = scene.GetGlobalMousePosition() - scene.RectSize / 2;
	}
}
