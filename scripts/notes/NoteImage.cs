using Godot;

public class NoteImage : NoteBase
{
	private TextureRect textureRect;

	public override void _Ready()
	{
		base._Ready();

		textureRect = GetNode<TextureRect>("./TextureRect");
	}

	public ImageTexture GetImage()
	{
		return (ImageTexture)textureRect.Texture;
	}

	public void SetImage(ImageTexture image)
	{
		textureRect.Texture = image;
	}
}
