using Godot;

public class NoteAudio : NoteBase
{
	private AudioStreamPlayer audioStreamPlayer;

	public override void _Ready()
	{
		base._Ready();

		audioStreamPlayer = GetNode<AudioStreamPlayer>("./AudioStreamPlayer");
	}

	public AudioStream GetAudio()
	{
		return audioStreamPlayer.Stream;
	}

	public void SetAudio(AudioStream audio)
	{
		audioStreamPlayer.Stream = audio;
	}
}
