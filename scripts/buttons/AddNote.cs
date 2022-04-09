using System;
using Godot;

public class AddNote : Button
{
	public override void _Ready()
	{
		Connect("button_down", this, nameof(OnButtonDown));
	}

	public void OnButtonDown()
	{
		NewNote();
	}

	public void NewNote()
	{
		Control scene = ResourceLoader.Load<PackedScene>("res://prefabs/note.tscn", noCache: true).Instance<Control>();
		Control persistentNodes = GetNode<Control>("/root/Node/Control/PersistentNodes");

		persistentNodes.AddChild(scene);
		scene.Owner = persistentNodes;
	}
}
