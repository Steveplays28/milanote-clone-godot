using System;
using Godot;

public class RemoveNote : Button
{
	public override void _Ready()
	{
		Connect("pressed", this, nameof(OnButtonPressed));
	}

	public void OnButtonPressed()
	{
		DeleteNote();
	}

	public void DeleteNote()
	{
		GetParent().QueueFree();
	}
}
