using Godot;
using Godot.Collections;

public class Save : Node
{
	public string savePath;
	public string saveFolder = "user://";
	public string saveFile = "Note1.tscn";

	public override void _Ready()
	{
		savePath = saveFolder + saveFile;
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

	public void SaveState()
	{
		Array nodesToSave = GetTree().GetNodesInGroup("Persist");
		PackedScene packedScene = new PackedScene();

		for (int i = 0; i < nodesToSave.Count; i++)
		{
			Node nodeToSave = (Node)nodesToSave[i];
			nodeToSave.Owner = nodeToSave.GetNode("..");

			foreach (Node child in GetAllChildren(nodeToSave))
			{
				child.Owner = nodeToSave;
			}

			packedScene.Pack(nodeToSave);
		}

		Error resourceSaverResult = ResourceSaver.Save(savePath, packedScene);
		if (resourceSaverResult == Error.Ok)
		{
			GD.Print($"Saved application state to disk at path: {savePath}");
		}
		else
		{
			GD.Print($"Failed saving application state to disk, reason: {resourceSaverResult}");
		}
	}

	public void LoadState()
	{
		PackedScene packedScene = (PackedScene)ResourceLoader.Load(savePath, noCache: true);
		if (packedScene == null)
		{
			GD.Print("Failed to load application state from disk, reason: no PackedScene found");
			return;
		}

		foreach (Node node in GetTree().GetNodesInGroup("Persist"))
		{
			if (node != null)
			{
				node.QueueFree();
				GD.Print("Loading application state from disk: destroyed existing version of saved asset");
			}
		}

		Node scene = packedScene.Instance();
		GetTree().Root.AddChild(scene);

		GD.Print($"Loaded application state from disk at path: {savePath}");
	}


	private Array GetAllChildren(Node node)
	{
		Array children = node.GetChildren();
		foreach (Node child in node.GetChildren())
		{
			if (child.GetChildCount() > 0)
			{
				children += child.GetChildren();
			}
		}

		return children;
	}
}
