using System;
using Godot;

public class ResizeButton : Button
{
	[Export] public Vector2 MinScale = new Vector2(0.5f, 0.5f);
	[Export] public Vector2 MaxScale = new Vector2(2f, 2f);

	private Vector2 mouseMovement;

	public override void _GuiInput(InputEvent inputEvent)
	{
		if (inputEvent is InputEventMouseMotion)
		{
			InputEventMouseMotion inputEventMouseMotion = inputEvent as InputEventMouseMotion;

			mouseMovement = inputEventMouseMotion.Relative;

			if (Input.IsMouseButtonPressed((int)ButtonList.Left) && GetParent<Control>().RectScale > MinScale && GetParent<Control>().RectScale < MaxScale)
			{
				GetParent<Control>().RectScale += mouseMovement;
			}
		}
	}
}
