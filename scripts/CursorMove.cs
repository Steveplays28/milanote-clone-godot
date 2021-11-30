using System;
using Godot;

public class CursorMove : Control
{
	private float mouseMovementX;
	private float mouseMovementY;

	private bool isSelected = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

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

			if (isSelected)
			{	
				float newPositionX = Mathf.Clamp(RectGlobalPosition.x + mouseMovementX, 0, GetViewport().Size.x - RectSize.x);
				float newPositionY = Mathf.Clamp(RectGlobalPosition.y + mouseMovementY, 0, GetViewport().Size.y - RectSize.y);
				
				RectGlobalPosition = new Vector2(newPositionX, newPositionY);
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
