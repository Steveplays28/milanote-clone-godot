using System;
using Godot;

public class NoteBase : Control
{
	private float mouseMovementX;
	private float mouseMovementY;

	private bool isMouseOver;
	private bool isSelected;

	private Vector2 newPosition = Vector2.Zero;

	public override void _Ready()
	{
		// Connect signals to functions
		Connect("mouse_entered", this, "_OnMouseEntered");
		Connect("mouse_exited", this, "_OnMouseExited");

		newPosition = RectGlobalPosition;
	}

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

		// GD.Print($"Mouse over: {isMouseOver} ----- Selected: {isSelected});
	}

	public override void _GuiInput(InputEvent inputEvent)
	{
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

		if (inputEvent is InputEventMouseMotion)
		{
			InputEventMouseMotion inputEventMouseMotion = inputEvent as InputEventMouseMotion;

			mouseMovementX = inputEventMouseMotion.Relative.x;
			mouseMovementY = inputEventMouseMotion.Relative.y;

			if (isMouseOver && isSelected)
			{
				// Vector2 newPosition = Vector2.Zero;

				newPosition.x += mouseMovementX;
				newPosition.y += mouseMovementY;

				Vector2 newPositionSnapped = Vector2.Zero;
				newPositionSnapped.x = Mathf.Clamp(newPosition.Snapped(new Vector2(32, 32)).x, 0, GetViewport().Size.x - RectSize.x);
				newPositionSnapped.y = Mathf.Clamp(newPosition.Snapped(new Vector2(32, 32)).y, 0, GetViewport().Size.y - RectSize.y);

				RectGlobalPosition = newPositionSnapped;
			}
		}
	}
}
