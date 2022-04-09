using Godot;

public class NoteManager : Node
{
	public override void _Input(InputEvent inputEvent)
	{
		base._Input(inputEvent);

		if (inputEvent is InputEventKey)
		{
			InputEventKey inputEventKey = inputEvent as InputEventKey;

			// Copy
			if ((OS.GetName() == "OSX" ? inputEventKey.Command : inputEventKey.Control) && inputEventKey.Scancode == (int)KeyList.C)
			{
				OS.Clipboard = "";
			}

			// Cut
			if ((OS.GetName() == "OSX" ? inputEventKey.Command : inputEventKey.Control) && inputEventKey.Scancode == (int)KeyList.C)
			{
				OS.Clipboard = "";
			}

			// Paste
			if ((OS.GetName() == "OSX" ? inputEventKey.Command : inputEventKey.Control) && inputEventKey.Scancode == (int)KeyList.R)
			{
				OS.Clipboard = "";
			}
		}
	}
}
