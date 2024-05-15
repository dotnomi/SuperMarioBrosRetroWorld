using Godot;
using System;

public partial class player_physics : CharacterBody2D
{
	[ExportGroup("Walking and Running")]
	[Export] private float _maxWalkSpeed = 80f;
	[Export] private float _maxSprintSpeed = 135f;

	[Export] private float _walkAccel = 337.5f;
	[Export] private float _stopDecel = 225f;
	[Export] private float _runAccel = 337.5f;

	[ExportGroup("Jumping")] 
	[Export] private float _jumpBufferTime = 0.1f;
	[Export] private float _coyoteTime = 0.1f;
	[Export] private float _jumpSpeed = 300f;
	[Export] private float _jumpSpeedIncr = 9.375f;
	[Export] private float _gravityWithoutJumpHeld = 1350f;
	[Export] private float _gravityWithJumpHeld = 675f;
	
	private float _gravity;
	private float _jumpBufferTimer;
	private float _coyoteTimer;

	private Sprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public override void _Process(double delta)
	{
		_jumpBufferTimer -= (float) delta;
		_coyoteTimer -= (float) delta;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (Input.IsActionJustPressed("Jump"))
		{
			_jumpBufferTimer = _jumpBufferTime;
			if (_coyoteTimer > 0)
			{
				velocity.Y = -_jumpSpeed;
			}
		}

		if (Input.IsActionPressed("Jump"))
		{
			_gravity = _gravityWithJumpHeld;
		}
		else
		{
			_gravity = _gravityWithoutJumpHeld;
		}
		
		if (IsOnFloor())
		{
			if (Input.IsActionJustPressed("Jump"))
			{
				velocity.Y = -_jumpSpeed;
			} else if (_jumpBufferTimer > 0)
			{
				velocity.Y = -_jumpSpeed;
			}

			_coyoteTimer = _coyoteTime;
		}
		
		velocity.Y += _gravity * (float) delta;
		
		// Horizontale Bewegung
		float xInput = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");

		float currentMaxSpeed = Input.GetActionStrength("Sprint") > 0 ? _maxSprintSpeed : _maxWalkSpeed;
		
		float accel = xInput != 0 ? (xInput > 0 ? _runAccel : _walkAccel) : _stopDecel;
		velocity.X = Mathf.MoveToward(velocity.X, xInput * currentMaxSpeed, accel * (float) delta);

		if (velocity.X != 0)
		{
			_sprite.FlipH = velocity.X < 0;
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
