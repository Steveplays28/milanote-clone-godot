using System;
using Godot;

public class RemoveNote : Button
{
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
