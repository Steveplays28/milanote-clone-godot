using System.IO;
using Godot;

public class NoteManager : Node
{
	[Export] public readonly string TextNotePath = "res://prefabs/note_text.tscn";
	[Export] public readonly string ImageNotePath = "res://prefabs/note_image.tscn";
	[Export] public readonly string AudioNotePath = "res://prefabs/note_audio.tscn";

	public override void _Ready()
	{
		base._Ready();

		GetTree().Connect("files_dropped", this, nameof(OnFilesDropped));
	}

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

#pragma warning disable
	private void OnFilesDropped(string[] files, int screen)
#pragma warning restore
	{
		GD.Print(files[0]);

		// Image files
		if (files[0].EndsWith("png") || files[0].EndsWith("jpg") || files[0].EndsWith("jpeg"))
		{
			Image image = new Image();
			image.Load(files[0]);
			ImageTexture imageTexture = new ImageTexture();
			imageTexture.CreateFromImage(image);

			CreateNote(imageTexture);
		}

		// Audio files
		if (files[0].EndsWith("mp3"))
		{
			AudioStreamMP3 audioStream = new AudioStreamMP3
			{
				Data = System.IO.File.ReadAllBytes(files[0])
			};

			GD.Print(System.IO.File.ReadAllBytes(files[0]).Length);

			CreateNote(audioStream);
		}
		else if (files[0].EndsWith("ogg"))
		{
			Godot.File file = new Godot.File();
			file.Open(files[0], Godot.File.ModeFlags.Read);
			byte[] data = file.GetBuffer((int)file.GetLen());

			AudioStreamOGGVorbis audioStream = new AudioStreamOGGVorbis()
			{
				Data = data
			};

			CreateNote(audioStream);
		}
		// else if (files[0].EndsWith("wav"))
		// {
		// 	AudioStreamSample audioStream = GD.Load<AudioStreamSample>(files[0]);

		// 	CreateNote(audioStream);
		// }
	}

	public void CreateNote(string text)
	{
		PackedScene packedScene = ResourceLoader.Load<PackedScene>(TextNotePath, noCache: true);
		Node prefab = packedScene.Instance();

		GetNode("/root/Node/Control/PersistentNodes").AddChild(prefab);
		prefab.Call(nameof(NoteText.SetText), text);
	}
	public void CreateNote(ImageTexture image)
	{
		PackedScene packedScene = ResourceLoader.Load<PackedScene>(ImageNotePath, noCache: true);
		Node prefab = packedScene.Instance();

		GetNode("/root/Node/Control/PersistentNodes").AddChild(prefab);
		prefab.Call(nameof(NoteImage.SetImage), image);
	}
	public void CreateNote(AudioStream audio)
	{
		PackedScene packedScene = ResourceLoader.Load<PackedScene>(AudioNotePath, noCache: true);
		Node prefab = packedScene.Instance();

		GetNode("/root/Node/Control/PersistentNodes").AddChild(prefab);
		prefab.Call(nameof(NoteAudio.SetAudio), audio);
	}
}
