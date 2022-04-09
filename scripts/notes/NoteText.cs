using Godot;

public class NoteText : NoteBase
{
	private Label textLabel;

	public override void _Ready()
	{
		base._Ready();

		textLabel = GetNode<Label>("./Label");
	}

	public string GetText()
	{
		return textLabel.Text;
	}

	public void SetText(string text)
	{
		textLabel.Text = text;
	}
}
