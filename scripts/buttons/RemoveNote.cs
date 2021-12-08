using System;
using Godot;

public class RemoveNote : Button
{
	public override void _Ready()
	{
		// Connect signals to functions
		Connect("pressed", this, "_OnButtonPressed");
	}

	// Signal
	public void _OnButtonPressed()
	{
		DeleteNote();
	}

	public void DeleteNote()
	{
		GetParent().QueueFree();
	}
}
