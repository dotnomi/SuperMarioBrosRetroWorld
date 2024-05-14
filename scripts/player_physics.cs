using Godot;
using System;

public partial class player_physics : CharacterBody2D
{
	[Export] private float _gravity = 2000f;
	
	[Export] private float _maxWalkSpeed = 80f;
	[Export] private float _maxSprintSpeed = 135f;

	[Export] private float _walkAccel = 337.5f;
	[Export] private float _stopDecel = 225f;
	[Export] private float _runAccel = 337.5f;

	[Export] private float _minJumpSpeed = 50f;
	[Export] private float _maxJumpSpeed = 550f;

	private float _currentJumpSpeed;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("Jump"))
			{
				_currentJumpSpeed = -_minJumpSpeed;
			}
			
			if (Input.IsActionPressed("Jump"))
			{
				if (_currentJumpSpeed > -_maxJumpSpeed)
				{
					_currentJumpSpeed -= 1000f * (float) delta;
				}
			}
			else
			{
				_currentJumpSpeed = 0f;
			}
		}
		else
		{
			_currentJumpSpeed = 0f;
		}

		velocity.Y = _currentJumpSpeed;
		velocity.Y += _gravity * (float) delta;
		
		// Horizontale Bewegung
		float xInput = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");

		float currentMaxSpeed = Input.GetActionStrength("Sprint") > 0 ? _maxSprintSpeed : _maxWalkSpeed;
		
		float accel = xInput != 0 ? (xInput > 0 ? _runAccel : _walkAccel) : _stopDecel;
		velocity.X = Mathf.MoveToward(velocity.X, xInput * currentMaxSpeed, accel * (float) delta);
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
