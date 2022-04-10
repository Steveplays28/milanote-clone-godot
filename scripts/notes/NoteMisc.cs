using Godot;

public class NoteMisc : NoteBase
{
	private File file;
	private TextureRect textureRect;

	public override void _Ready()
	{
		base._Ready();

		GetNode("./Player/OpenButton").Connect("pressed", this, nameof(OpenFile));
		GetNode("./Player/DownloadButton").Connect("pressed", this, nameof(DownloadFile));

		file = new File();
		textureRect = GetNode<TextureRect>("./TextureRect");
	}

	public File GetFile()
	{
		return file;
	}

	public void SetFile(File newFile)
	{
		file = newFile;
	}

	public ImageTexture GetThumbnail()
	{
		return (ImageTexture)textureRect.Texture;
	}

	public void SetThumbnail(ImageTexture thumbnail)
	{
		textureRect.Texture = thumbnail;
	}

	public void OpenFile()
	{
		OS.ShellOpen(file.GetPathAbsolute());
	}

	public void DownloadFile()
	{
		OS.Execute("cp", new string[] { file.GetPathAbsolute(), "/home/dspaargaren/Downloads" });
	}
}
