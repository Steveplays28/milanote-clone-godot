using System;
using Godot;

public class NoteBase : Control
{
	private float mouseMovementX;
	private float mouseMovementY;

	private bool isMouseOver;
	private bool isSelected;

	// Signal
	public void _OnMouseEntered()
	{
		isMouseOver = true;
	}

	// Signal
	public void _OnMouseExited()
	{
		isMouseOver = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		mouseMovementX = 0f;
		mouseMovementY = 0f;
	}

	public override void _GuiInput(InputEvent inputEvent)
	{
		// Mouse input
		if (inputEvent is InputEventMouseMotion)
		{
			InputEventMouseMotion inputEventMouseMotion = inputEvent as InputEventMouseMotion;

			mouseMovementX = inputEventMouseMotion.Relative.x;
			mouseMovementY = inputEventMouseMotion.Relative.y;

			if (isMouseOver && isSelected)
			{
				Vector2 newPosition = Vector2.Zero;

				newPosition.x = Mathf.Clamp(RectGlobalPosition.x + mouseMovementX, 0, GetViewport().Size.x - RectSize.x);
				newPosition.y = Mathf.Clamp(RectGlobalPosition.y + mouseMovementY, 0, GetViewport().Size.y - RectSize.y);

				RectGlobalPosition = newPosition;
			}
		}

		if (inputEvent is InputEventMouseButton)
		{
			InputEventMouseButton inputEventMouseButton = inputEvent as InputEventMouseButton;

			if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Left && inputEventMouseButton.Pressed)
			{
				isSelected = true;
			}
			else if (inputEventMouseButton.ButtonIndex == (int)ButtonList.Left && !inputEventMouseButton.Pressed)
			{
				isSelected = false;
			}
		}
	}
}
