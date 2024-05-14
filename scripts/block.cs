using Godot;
using System;
using System.Collections.Generic;

public partial class block : StaticBody2D
{
	[Export] private float _gravity = 2925f;
	[Export] private float _initialBounceSpeed = -240f;
	[Export] private bool _isHittable = false;

	private Timer _bounceTimer;
	private Node2D _sprite;

	private Vector2 _originalSpritePosition;
	private float _bounceSpeed;
	
	public override void _Ready()
	{
		_bounceTimer = GetNode<Timer>("BounceTimer");
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		_originalSpritePosition = _sprite.Position;
	}
	
	public override void _Process(double delta)
	{
		if (_isHittable)
		{
			if (_bounceTimer.IsStopped())
			{
				_bounceSpeed = 0f;
				_sprite.Position = _originalSpritePosition;
			}
			else
			{
				_bounceSpeed += _gravity * (float) delta;
				AddYPosition(_bounceSpeed * (float) delta);
			}
		}
	}
	
	private void Bounce()
	{
		if (_isHittable)
		{
			_bounceSpeed = _initialBounceSpeed;
			_bounceTimer.Start();
		}
	}

	private void AddYPosition(float y)
	{
		_sprite.Position = new Vector2(_sprite.Position.X, _sprite.Position.Y + y);
	}

	private void _OnTopHitZoneEntered(Area2D area)
	{
		Bounce();
	}
	
	private void _OnBottomHitZoneEntered(Area2D area)
	{
		Bounce();
	}
	
	private void _OnLeftHitZoneEntered(Area2D area)
	{
		Bounce();
	}
	
	private void _OnRightHitZoneEntered(Area2D area)
	{
		Bounce();
	}
}
