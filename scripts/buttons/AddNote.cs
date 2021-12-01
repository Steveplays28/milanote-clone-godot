using System;
using Godot;

public class AddNote : Button
{
	// Signal
	public void _OnButtonPressed()
	{
		NewNote();
	}

	public void NewNote()
	{
		var note = ResourceLoader.Load<PackedScene>("res://premade_assets/note.tscn", noCache: true).Instance();
		Control controlNode = GetNode<Control>("/root/Node/Control");
		controlNode.AddChild(note);
		note.Owner = controlNode;
	}
}
