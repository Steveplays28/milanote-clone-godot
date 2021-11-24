using System;
using Godot;
using Godot.Collections;

public class Save : Node
{
	public string saveFileName = "Note";

	public override void _Ready()
	{
		LoadState();

		base._Ready();
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (inputEvent is InputEventKey)
		{
			InputEventKey inputEventKey = inputEvent as InputEventKey;

			if ((OS.GetName() == "OSX" ? inputEventKey.Command : inputEventKey.Control) && inputEventKey.Scancode == (int)KeyList.S)
			{
				SaveState();
			}

			if ((OS.GetName() == "OSX" ? inputEventKey.Command : inputEventKey.Control) && inputEventKey.Scancode == (int)KeyList.R)
			{
				LoadState();
			}
		}

		base._Input(inputEvent);
	}

	public void LoadState()
	{
		var saveFile = new File();
		if (!saveFile.FileExists($"user://{saveFileName}.save"))
		{
			GD.PrintErr("Error loading state: no save file to load from.");
			return;
		}

		var saveNodes = GetTree().GetNodesInGroup("Persist");
		foreach (Control saveNode in saveNodes)
		{
			saveNode.QueueFree();
		}

		saveFile.Open($"user://{saveFileName}.save", File.ModeFlags.Read);
		while (saveFile.GetPosition() < saveFile.GetLen())
		{
			// Get the saved dictionary from the next line in the save file
			var nodeData = new Dictionary<string, object>((Dictionary)JSON.Parse(saveFile.GetLine()).Result);

			// Create the object, add it to the tree, and set its position
			var newObjectScene = (PackedScene)ResourceLoader.Load(nodeData["Filename"].ToString());
			var newObject = (Node)newObjectScene.Instance();
			GetNode(nodeData["Parent"].ToString()).AddChild(newObject);

			// Set the remaining variables
			foreach (System.Collections.Generic.KeyValuePair<string, object> entry in nodeData)
			{
				var entryKey = entry.Key.ToString();
				if (entryKey != "Filename" && entryKey != "Parent")
				{
					newObject.Set(entryKey, entry.Value);
				}
			}
		}

		GD.Print($"Loaded state from disk at path: user://{saveFileName}.save");
	}

	public void SaveState()
	{
		var saveFile = new File();
		saveFile.Open($"user://{saveFileName}.save", File.ModeFlags.Write);

		var saveNodes = GetTree().GetNodesInGroup("Persist");
		foreach (Control saveNode in saveNodes)
		{
			// Convert node to dictionary
			var nodeData = ControlNodeToDictionary(saveNode);

			// Store the dictionary in the save file
			saveFile.StoreLine(JSON.Print(nodeData));
			saveFile.Flush();
		}

		saveFile.Close();
		GD.Print($"Saved state to disk at path: user://{saveFileName}.save");
	}

	private Godot.Collections.Dictionary<string, object> ControlNodeToDictionary(Control node)
	{
		return new Godot.Collections.Dictionary<string, object>()
		{
			{ "Filename", node.Filename },
			{ "Parent", node.GetParent().GetPath() },
			{ "RectPosition.x", node.RectPosition.x }, // Vector2 is not supported by JSON
        	{ "RectPosition.y", node.RectPosition.y }, // --------------------------------
		};
	}
}
